//ArtifactEditionXml.cs
using System.Xml.Serialization;

namespace Museum.Collection.Catalog.Infrastructure.XmlModels;

public sealed class ArtifactEditionXml
{
    [XmlElement("id")]
    public Guid Id { get; set; }

    [XmlElement("artifact_guid")]
    public Guid ArtifactGuid { get; set; }

    [XmlElement("version")]
    public string Version { get; set; } = "";

    [XmlElement("language")]
    public string Language { get; set; } = "";

    [XmlElement("display_label")]
    public string DisplayLabel { get; set; } = "";
}