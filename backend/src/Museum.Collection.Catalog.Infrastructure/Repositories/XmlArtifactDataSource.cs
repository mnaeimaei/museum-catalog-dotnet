using System.Xml.Serialization;
using Museum.Collection.Catalog.Domain.Entities;
using Museum.Collection.Catalog.Infrastructure.XmlModels;

namespace Museum.Collection.Catalog.Infrastructure.Repositories;

public sealed class XmlArtifactDataSource
{
    private readonly string _xmlFilePath;
    private readonly XmlSerializer _serializer;

    public XmlArtifactDataSource(string xmlFilePath)
    {
        _xmlFilePath = xmlFilePath;
        _serializer = new XmlSerializer(typeof(ArtifactsDocumentXml));
    }

    /// File loading (File → file read stream)
    private FileStream LoadFile()
    {
        if (!File.Exists(_xmlFilePath))
            throw new FileNotFoundException("artifacts.xml not found.", _xmlFilePath);

        return File.OpenRead(_xmlFilePath);
    }

    /// XML deserialization (file read stream → persistence model)
    private ArtifactsDocumentXml Deserialize(Stream stream)
    {
        return (ArtifactsDocumentXml?)_serializer.Deserialize(stream)
            ?? throw new InvalidOperationException("Invalid artifacts XML format.");
    }

    /// PersistenceModel → DomainEntity mapping
    private static Artifact MapToDomain(ArtifactXml x)
    {
        return new Artifact
        {
            Id = x.Id,
            Category = x.Category,
            Title = x.Title,
            Description = x.Description,
            AccessionNumber = x.AccessionNumber,
            Editions = (x.Editions ?? new List<ArtifactEditionXml>())
                .Select(e => new ArtifactEdition
                {
                    Id = e.Id,
                    ArtifactGuid = e.ArtifactGuid,
                    Version = e.Version,
                    Language = e.Language,
                    DisplayLabel = e.DisplayLabel
                })
                .ToList()
        };
    }

    /// DomainEntity → PersistenceModel mapping
    /// Not used in this project
    private static ArtifactXml MapToPersistence(Artifact artifact)
    {
        return new ArtifactXml
        {
            Id = artifact.Id,
            Category = artifact.Category,
            Title = artifact.Title,
            Description = artifact.Description,
            AccessionNumber = artifact.AccessionNumber,
            Editions = (artifact.Editions ?? new List<ArtifactEdition>())
                .Select(e => new ArtifactEditionXml
                {
                    Id = e.Id,
                    ArtifactGuid = e.ArtifactGuid,
                    Version = e.Version,
                    Language = e.Language,
                    DisplayLabel = e.DisplayLabel
                })
                .ToList()
        };
    }

    /// File saving (file → write stream)
    /// Not used in this project
    private FileStream SaveFile()
    {
        return File.Create(_xmlFilePath);
    }

    /// XML serialization (persistence model → file)
    /// Not used in this project
    private void Serialize(ArtifactsDocumentXml document, Stream stream)
    {
        _serializer.Serialize(stream, document);
    }

    public List<Artifact> LoadArtifacts()
    {
        using var stream = LoadFile();
        var document = Deserialize(stream);

        return (document.Items ?? new List<ArtifactXml>())
            .Select(MapToDomain)
            .ToList();
    }
}

