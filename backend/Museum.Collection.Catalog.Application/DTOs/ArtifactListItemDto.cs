namespace Museum.Collection.Catalog.Application.Dtos;

public sealed record ArtifactListItemDto(
    Guid Id,
    string Category,
    string Title,
    string AccessionNumber);