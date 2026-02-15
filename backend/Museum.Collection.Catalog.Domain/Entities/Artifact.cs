//Artifact.cs
using System.Xml.Serialization;

namespace Museum.Collection.Catalog.Domain.Entities;

public sealed class Artifact
{
    [XmlElement("id")]
    public Guid Id { get; init; }

    [XmlElement("category")]
    public string Category { get; init; } = "";

    [XmlElement("title")]
    public string Title { get; init; } = "";

    [XmlElement("description")]
    public string Description { get; init; } = "";

    [XmlElement("accession_number")]
    public string AccessionNumber { get; init; } = "";

    // <editions> <edition> ... </edition> </editions>
    [XmlArray("editions")]
    [XmlArrayItem("edition")]
    public List<ArtifactEdition> Editions { get; init; } = new();
}
