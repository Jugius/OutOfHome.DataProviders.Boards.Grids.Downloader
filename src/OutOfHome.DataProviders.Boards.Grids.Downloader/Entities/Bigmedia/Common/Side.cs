using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
public class Side
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("doorsNo")]
    public int? DoorsNo { get; set; }

    [JsonPropertyName("supplierNo")]
    public string SupplierNo { get; set; }

    [JsonPropertyName("num")]
    public long Num { get; set; }

    [JsonPropertyName("faceId")]
    public long FaceId { get; set; }

    [JsonPropertyName("occ")]
    public string Occ { get; set; }
}
