//ArtifactXml.cs
using System.Xml.Serialization;

namespace Museum.Collection.Catalog.Infrastructure.XmlModels;

public sealed class ArtifactXml
{
    [XmlElement("id")]
    public Guid Id { get; set; }

    [XmlElement("category")]
    public string Category { get; set; } = "";

    [XmlElement("title")]
    public string Title { get; set; } = "";

    [XmlElement("description")]
    public string Description { get; set; } = "";

    [XmlElement("accession_number")]
    public string AccessionNumber { get; set; } = "";

    [XmlArray("editions")]
    [XmlArrayItem("edition")]
    public List<ArtifactEditionXml> Editions { get; set; } = new();
}