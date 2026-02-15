//ArtifactEdition.cs
using System.Xml.Serialization;

namespace Museum.Collection.Catalog.Domain.Entities;

public sealed class ArtifactEdition
{
    [XmlElement("id")]
    public Guid Id { get; init; }

    [XmlElement("artifact_guid")]
    public Guid ArtifactGuid { get; init; }

    // keep as string: "1", "1.1", "2.3"
    [XmlElement("version")]
    public string Version { get; init; } = "";

    [XmlElement("language")]
    public string Language { get; init; } = "";

    [XmlElement("display_label")]
    public string DisplayLabel { get; init; } = "";
}
