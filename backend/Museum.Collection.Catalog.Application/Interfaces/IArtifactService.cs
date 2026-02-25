using Museum.Collection.Catalog.Application.Dtos;
using Museum.Collection.Catalog.Application.Queries;

namespace Museum.Collection.Catalog.Application.Services;

public interface IArtifactService
{
    Task<GetArtifactsResponseDto> GetArtifactsAsync(GetArtifactsQuery query);
    Task<ArtifactDetailsDto?> GetByIdAsync(Guid id);
    Task<ArtifactEditionDto?> GetEditionAsync(Guid artifactId, Guid editionId);
}