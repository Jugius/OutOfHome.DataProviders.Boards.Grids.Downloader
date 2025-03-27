using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class ConstructType
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("boardCount")]
    public int BoardCount { get; set; }
}
