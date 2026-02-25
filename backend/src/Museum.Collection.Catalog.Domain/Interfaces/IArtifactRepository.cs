//IArtifactRepository.cs


using Museum.Collection.Catalog.Domain.Entities;

namespace Museum.Collection.Catalog.Domain.Interfaces;

public interface IArtifactRepository : IRepository<Artifact>
{
    Task<IReadOnlyList<Artifact>> GetByCategoryAsync(string category);

    Task<IReadOnlyList<Artifact>> GetByAccessionNumberAsync(string accessionNumber);

    Task<ArtifactEdition?> GetEditionAsync(Guid artifactId, Guid editionId);
}
