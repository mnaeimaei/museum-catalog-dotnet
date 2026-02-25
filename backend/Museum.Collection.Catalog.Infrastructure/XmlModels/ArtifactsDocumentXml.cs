//ArtifactsDocumentXml.cs
using System.Xml.Serialization;

namespace Museum.Collection.Catalog.Infrastructure.XmlModels;

[XmlRoot("artifacts")]
public sealed class ArtifactsDocumentXml
{
    [XmlElement("artifact")]
    public List<ArtifactXml> Items { get; set; } = new();
}