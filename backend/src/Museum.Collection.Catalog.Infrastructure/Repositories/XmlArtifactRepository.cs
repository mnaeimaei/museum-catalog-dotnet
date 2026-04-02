using System.Linq.Expressions;
using Museum.Collection.Catalog.Domain.Entities;
using Museum.Collection.Catalog.Domain.Interfaces;

namespace Museum.Collection.Catalog.Infrastructure.Repositories;

public sealed class XmlArtifactRepository : IArtifactRepository
{
    private readonly List<Artifact> _artifacts;

    public XmlArtifactRepository(string xmlFilePath)
    {
        var dataSource = new XmlArtifactDataSource(xmlFilePath);
        _artifacts = dataSource.LoadArtifacts();
    }

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
        category ??= string.Empty;

        var result = _artifacts
            .Where(a => a.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult((IReadOnlyList<Artifact>)result);
    }

    public Task<IReadOnlyList<Artifact>> GetByAccessionNumberAsync(string accessionNumber)
    {
        accessionNumber ??= string.Empty;

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