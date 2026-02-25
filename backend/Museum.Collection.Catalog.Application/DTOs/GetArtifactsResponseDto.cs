namespace Museum.Collection.Catalog.Application.Dtos;

public sealed record GetArtifactsResponseDto(
    int Page,
    int PageSize,
    int Total,
    IReadOnlyList<ArtifactListItemDto> Data);