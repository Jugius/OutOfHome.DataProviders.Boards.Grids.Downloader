using OutOfHome.DataProviders.Boards.Grids.Downloader.JsonConverters;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
public class Board
{
    [JsonPropertyName("address")]
    public string Address { get; set; }

    //[JsonPropertyName("id")]
    //public long Id { get; set; }

    //[JsonPropertyName("num")]
    //public long Num { get; set; }

    [JsonPropertyName("id_city")]
    public int IdCity { get; set; }

    [JsonPropertyName("id_size")]
    public int IdSize { get; set; }

    [JsonPropertyName("id_network")]
    public int IdNetwork { get; set; }

    [JsonPropertyName("lon")]
    [JsonConverter(typeof(StringToDoubleConverter))]
    public double? Lon { get; set; }

    [JsonPropertyName("lat")]
    [JsonConverter(typeof(StringToDoubleConverter))]
    public double? Lat { get; set; }

    [JsonPropertyName("grp")]
    public float? Grp { get; set; }

    [JsonPropertyName("ots")]
    public int? Ots { get; set; }

    //[JsonProperty("doors_no")]
    //[JsonPropertyName("doors_no")]
    //public int? DoorsNo { get; set; }

    //[JsonProperty("pos")]
    //[JsonPropertyName("pos")]
    //public long Pos { get; set; }

    [JsonPropertyName("angle")]
    public int? Angle { get; set; }

    [JsonPropertyName("id_catab")]
    public byte? IdCatab { get; set; }

    //[JsonProperty("rating")]
    //[JsonPropertyName("rating")]
    //public long? Rating { get; set; }

    //[JsonProperty("busy")]
    //[JsonPropertyName("busy")]
    //public long Busy { get; set; }

    [JsonPropertyName("light")]
    public string Light { get; set; }

    //[JsonProperty("streets")]
    //[JsonPropertyName("streets")]
    //public string Streets { get; set; }

    [JsonPropertyName("price")]
    public float? Price { get; set; }

    [JsonPropertyName("supplier_sidetype")]
    public string SupplierSidetype { get; set; }

    [JsonPropertyName("sides")]
    public Side[] Sides { get; set; }

    [JsonPropertyName("photo_url")]
    public string PhotoUrl { get; set; }

    [JsonPropertyName("schema_url")]
    public string SchemaUrl { get; set; }

    [JsonPropertyName("id_supplier")]
    public int IdSupplier { get; set; }

    //[JsonProperty("supplier_sn")]
    //[JsonPropertyName("supplier_sn")]
    //public string SupplierSn { get; set; }
}
