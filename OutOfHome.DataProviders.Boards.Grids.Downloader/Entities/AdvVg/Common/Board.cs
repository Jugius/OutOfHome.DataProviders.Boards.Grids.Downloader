using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;
public class Board
{

    [JsonPropertyName("code")]
    public string SupplierCode { get; set; }


    [JsonPropertyName("city")]
    public string City { get; set; }


    [JsonPropertyName("dist")]
    public string District { get; set; }


    [JsonPropertyName("adr")]
    public string Address { get; set; }


    [JsonPropertyName("area")]
    public string Area { get; set; }


    [JsonPropertyName("side")]
    public string Side { get; set; }


    [JsonPropertyName("size")]
    public string Size { get; set; }


    [JsonPropertyName("type")]
    public string Type { get; set; }


    [JsonPropertyName("lig")]
    public byte? Lighting { get; set; }


    [JsonPropertyName("dix")]
    public int? DoorsDix { get; set; }


    [JsonPropertyName("ots")]
    public int? Ots { get; set; }


    [JsonPropertyName("grp")]
    public float? Grp { get; set; }


    [JsonPropertyName("st")]
    public DateOnly VisibleStart { get; set; }


    [JsonPropertyName("end")]
    public DateOnly VisibleEnd { get; set; }


    [JsonPropertyName("occ")]
    public List<OccupationPeriod> OccupationPeriods { get; set; }


    [JsonPropertyName("pr")]
    public int Price { get; set; }   


    [JsonPropertyName("photoId")]
    public string PhotoId { get; set; }
}
