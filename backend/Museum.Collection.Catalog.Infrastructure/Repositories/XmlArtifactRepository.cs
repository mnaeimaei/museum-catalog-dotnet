//XmlArtifactRepository.cs

using System.Linq.Expressions;
using System.Xml.Serialization;
using Museum.Collection.Catalog.Domain.Entities;
using Museum.Collection.Catalog.Domain.Interfaces;
using Museum.Collection.Catalog.Infrastructure.Xml;

namespace Museum.Collection.Catalog.Infrastructure.Repositories;

public sealed class XmlArtifactRepository : IArtifactRepository
{
    private readonly List<Artifact> _artifacts;

    public XmlArtifactRepository(string xmlFilePath)
    {
        _artifacts = ArtifactXmlLoader.Load(xmlFilePath).ToList();
    }

    public Task<Artifact?> SingleAsync(Guid id)
    {
        var result = _artifacts.SingleOrDefault(a => a.Id == id);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<Artifact>> WhereAsync(Expression<Func<Artifact, bool>> predicate)
    {
        var result = _artifacts.AsQueryable()
            .Where(predicate)
            .ToList();

        return Task.FromResult((IReadOnlyList<Artifact>)result);
    }

    public Task<IReadOnlyList<Artifact>> WhereAsync(
        Expression<Func<Artifact, bool>> predicate,
        Func<IQueryable<Artifact>, IOrderedQueryable<Artifact>> orderBy)
    {
        var result = orderBy(_artifacts.AsQueryable().Where(predicate))
            .ToList();

        return Task.FromResult((IReadOnlyList<Artifact>)result);
    }

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
