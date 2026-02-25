//XmlArtifactRepository.cs
using System.Linq.Expressions;
using System.Xml.Serialization;
using Museum.Collection.Catalog.Domain.Entities;
using Museum.Collection.Catalog.Domain.Interfaces;
using Museum.Collection.Catalog.Infrastructure.XmlModels;

namespace Museum.Collection.Catalog.Infrastructure.Repositories;

public sealed class XmlArtifactRepository : IArtifactRepository
{
    private readonly List<Artifact> _artifacts;

    public XmlArtifactRepository(string xmlFilePath)
    {
        if (!File.Exists(xmlFilePath))
            throw new FileNotFoundException("artifacts.xml not found.", xmlFilePath);

        var serializer = new XmlSerializer(typeof(ArtifactsDocumentXml));

        using var stream = File.OpenRead(xmlFilePath);

        var document = (ArtifactsDocumentXml?)serializer.Deserialize(stream)
            ?? throw new InvalidOperationException("Invalid artifacts XML format.");

        _artifacts = (document.Items ?? new List<ArtifactXml>())
            .Select(ToDomain)
            .ToList();
    }

    private static Artifact ToDomain(ArtifactXml x) => new()
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

    public Task<Artifact?> SingleAsync(Guid id)
        => Task.FromResult(_artifacts.SingleOrDefault(a => a.Id == id));

    public Task<IReadOnlyList<Artifact>> WhereAsync(Expression<Func<Artifact, bool>> predicate)
        => Task.FromResult((IReadOnlyList<Artifact>)_artifacts.AsQueryable().Where(predicate).ToList());

    public Task<IReadOnlyList<Artifact>> WhereAsync(
        Expression<Func<Artifact, bool>> predicate,
        Func<IQueryable<Artifact>, IOrderedQueryable<Artifact>> orderBy)
        => Task.FromResult((IReadOnlyList<Artifact>)orderBy(_artifacts.AsQueryable().Where(predicate)).ToList());

    public Task<IReadOnlyList<Artifact>> GetByCategoryAsync(string category)
    {
        category ??= "";
        var result = _artifacts
            .Where(a => a.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Task.FromResult((IReadOnlyList<Artifact>)result);
    }

    public Task<IReadOnlyList<Artifact>> GetByAccessionNumberAsync(string accessionNumber)
    {
        accessionNumber ??= "";
        var result = _artifacts
            .Where(a => a.AccessionNumber.Equals(accessionNumber, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Task.FromResult((IReadOnlyList<Artifact>)result);
    }

    public Task<ArtifactEdition?> GetEditionAsync(Guid artifactId, Guid editionId)
    {
        var artifact = _artifacts.SingleOrDefault(a => a.Id == artifactId);
        var edition = artifact?.Editions.SingleOrDefault(e => e.Id == editionId);
        return Task.FromResult(edition);
    }
}