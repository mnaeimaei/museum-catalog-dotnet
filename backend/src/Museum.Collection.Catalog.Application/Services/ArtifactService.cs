using Museum.Collection.Catalog.Application.Dtos;
using Museum.Collection.Catalog.Application.Mappings;
using Museum.Collection.Catalog.Application.Queries;
using Museum.Collection.Catalog.Application.Validators;
using Museum.Collection.Catalog.Domain.Interfaces;

namespace Museum.Collection.Catalog.Application.Services;

public sealed class ArtifactService : IArtifactService
{
    private readonly IArtifactRepository _repo;

    public ArtifactService(IArtifactRepository repo)
    {
        _repo = repo;
    }

    public async Task<GetArtifactsResponseDto> GetArtifactsAsync(GetArtifactsQuery query)
    {
        query.Normalize();
        GetArtifactsQueryValidator.Validate(query);

        var items = await _repo.WhereAsync(a =>
            (query.Title == null || a.Title.Contains(query.Title, StringComparison.OrdinalIgnoreCase)) &&
            (query.AccessionNumber == null || a.AccessionNumber.Contains(query.AccessionNumber, StringComparison.OrdinalIgnoreCase))
        );

        var desc = string.Equals(query.Direction, "desc", StringComparison.OrdinalIgnoreCase);

        items = query.Sort switch
        {
            "title" => desc ? items.OrderByDescending(a => a.Title).ToList() : items.OrderBy(a => a.Title).ToList(),
            "category" => desc ? items.OrderByDescending(a => a.Category).ToList() : items.OrderBy(a => a.Category).ToList(),
            "accession_number" or "accessionnumber" => desc
                ? items.OrderByDescending(a => a.AccessionNumber).ToList()
                : items.OrderBy(a => a.AccessionNumber).ToList(),
            _ => items
        };

        var total = items.Count;

        var paged = items
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(a => a.ToListItemDto())
            .ToList();

        return new GetArtifactsResponseDto(
            query.Page,
            query.PageSize,
            total,
            paged
        );
    }

    public async Task<ArtifactDetailsDto?> GetByIdAsync(Guid id)
    {
        var artifact = await _repo.SingleAsync(id);
        return artifact?.ToDetailsDto();
    }

    public async Task<ArtifactEditionDto?> GetEditionAsync(Guid artifactId, Guid editionId)
    {
        var edition = await _repo.GetEditionAsync(artifactId, editionId);
        return edition?.ToEditionDto();
    }
}