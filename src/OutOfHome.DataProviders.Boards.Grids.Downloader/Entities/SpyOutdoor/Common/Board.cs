using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class Board
{
    [JsonPropertyName("boardId")]
    public int BoardId { get; set; }

    [JsonPropertyName("townId")]
    public int TownId { get; set; }

    [JsonPropertyName("constructTypeId")]
    public int ConstructTypeId { get; set; }

    [JsonPropertyName("lat")]
    public double? Lat { get; set; }

    [JsonPropertyName("lon")]
    public double? Lon { get; set; }

    [JsonPropertyName("viewPoint")]
    public string ViewPoint { get; set; }

    [JsonPropertyName("states")]
    public List<string> States { get; set; }

    [JsonPropertyName("operatorKey")]
    public string OperatorKey { get; set; }

    [JsonPropertyName("ots")]
    public object Ots { get; set; }

    [JsonPropertyName("grp")]
    public object Grp { get; set; }

    [JsonPropertyName("photoHash")]
    public string PhotoHash { get; set; }

    [JsonPropertyName("schemeHash")]
    public string SchemeHash { get; set; }

    [JsonPropertyName("price")]
    public int? Price { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("boardFormat")]
    public string BoardFormat { get; set; }

    [JsonPropertyName("side")]
    public string Side { get; set; }

    [JsonPropertyName("posterMaterial")]
    public string PosterMaterial { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("light")]
    public int Light { get; set; }
}