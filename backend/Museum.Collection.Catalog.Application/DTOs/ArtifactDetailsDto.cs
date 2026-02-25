namespace Museum.Collection.Catalog.Application.Dtos;

public sealed record ArtifactDetailsDto(
    Guid Id,
    string Category,
    string Title,
    string Description,
    string AccessionNumber,
    IReadOnlyList<ArtifactEditionDto> Editions);