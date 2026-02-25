namespace Museum.Collection.Catalog.Application.Dtos;

public sealed record ArtifactEditionDto(
    Guid EditionId,
    Guid ArtifactGuid,
    string Version,
    string Language,
    string DisplayLabel);