using System.Globalization;
using System.Xml.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public class OctagonBoard
{
    private static readonly NumberFormatInfo parsePointFormatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
    private const NumberStyles parseStyle = NumberStyles.AllowDecimalPoint;

    [XmlAttribute(AttributeName = "FaceNumber")]
    public string SupplierCode { get; set; }

    [XmlAttribute(AttributeName = "SiteAddress")]
    public string Street { get; set; }

    [XmlAttribute(AttributeName = "CityId")]
    public string CityId { get; set; }

    [XmlAttribute(AttributeName = "MediaTypeName")]
    public string TypeSizeName { get; set; }

    [XmlAttribute(AttributeName = "Face")]
    public string Side { get; set; }

    [XmlAttribute(AttributeName = "Illuminated")]
    public byte Lighting { get; set; }

    [XmlAttribute(AttributeName = "Price")]
    public float Price { get; set; }

    [XmlAttribute(AttributeName = "Latitude")]
    public double Latitude { get; set; }

    [XmlAttribute(AttributeName = "Longitude")]
    public double Longitude { get; set; }

    [XmlAttribute(AttributeName = "Direction")]
    public short Direction { get; set; }


    [XmlAttribute(AttributeName = "PeriodFrom", DataType = "date")]
    public DateTime PeriodFrom { get; set; }

    [XmlAttribute(AttributeName = "PeriodTo", DataType = "date")]
    public DateTime PeriodTo { get; set; }

    [XmlAttribute(AttributeName = "FaceStatus")]
    public byte OccupationStatus { get; set; }


    [XmlAttribute(AttributeName = "EsparCode")]
    public string DoorsDix { get; set; }

    [XmlAttribute(AttributeName = "GRP")]
    public float Grp { get; set; }

    [XmlAttribute(AttributeName = "OTS")]
    public float Ots { get; set; }



    //[XmlAttribute(AttributeName = "MediaTypeId")]
    //[JsonProperty("typesizeId")]
    //public string TypeSizeId { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string Oid { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string SiteId { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string SiteNumber { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string MediaTypeId { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string PrimaryImageId { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string ProductId { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string ProductName { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string SiteMediaTypeIds { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string SiteFaceIds { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string SiteFaces { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public string SiteDirections { get; set; }        

    //[System.Xml.Serialization.XmlAttribute()]
    //public string BasketRecordId { get; set; }

    //[System.Xml.Serialization.XmlAttribute()]
    //public byte Selected { get; set; }        

    //[XmlAttribute(AttributeName = "CityName")]
    //public string cityStr { get; set; }
}
