//ArtifactXmlLoader.cs
using System.Xml.Serialization;
using Museum.Collection.Catalog.Domain.Entities;

namespace Museum.Collection.Catalog.Infrastructure.Xml;

public static class ArtifactXmlLoader
{
    public static IReadOnlyList<Artifact> Load(string xmlFilePath)
    {
        if (!File.Exists(xmlFilePath))
            throw new FileNotFoundException("artifacts.xml not found.", xmlFilePath);

        var serializer = new XmlSerializer(typeof(ArtifactsDocument));

        using var stream = File.OpenRead(xmlFilePath);

        var document = (ArtifactsDocument?)serializer.Deserialize(stream);

        var artifacts = document?.Items ?? new List<Artifact>();

        return artifacts;
    }
}


