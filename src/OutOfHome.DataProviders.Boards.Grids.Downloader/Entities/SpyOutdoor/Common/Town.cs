using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class Town
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("regionId")]
    public int RegionId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("lat")]
    public double? Lat { get; set; }

    [JsonPropertyName("lon")]
    public double? Lon { get; set; }

    [JsonPropertyName("boardCount")]
    public int BoardCount { get; set; }
}
