//ArtifactsDocument.cs
using System.Xml.Serialization;

namespace Museum.Collection.Catalog.Domain.Entities;

[XmlRoot("artifacts")]
public sealed class ArtifactsDocument
{
    [XmlElement("artifact")]
    public List<Artifact> Items { get; set; } = new();
}
