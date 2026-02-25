//ArtifactEdition.cs
namespace Museum.Collection.Catalog.Domain.Entities;

public sealed class ArtifactEdition
{
    public Guid Id { get; init; }
    public Guid ArtifactGuid { get; init; }
    public string Version { get; init; } = "";
    public string Language { get; init; } = "";
    public string DisplayLabel { get; init; } = "";
}