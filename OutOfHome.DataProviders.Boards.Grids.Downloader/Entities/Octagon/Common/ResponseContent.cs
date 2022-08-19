using System.Xml.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;


[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", ElementName = "face", IsNullable = false)]
public class ResponseContent
{
    [XmlElement("item")]
    public OctagonBoard[] Boards { get; set; }
}
