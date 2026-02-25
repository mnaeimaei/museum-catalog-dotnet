//Artifact.cs
namespace Museum.Collection.Catalog.Domain.Entities;

public sealed class Artifact
{
    public Guid Id { get; init; }
    public string Category { get; init; } = "";
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
    public string AccessionNumber { get; init; } = "";
    public List<ArtifactEdition> Editions { get; init; } = new();
}