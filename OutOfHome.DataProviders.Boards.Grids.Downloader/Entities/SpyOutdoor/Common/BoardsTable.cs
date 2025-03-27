using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class BoardsTable
{
    [JsonPropertyName("boards")]
    public List<Board> Boards { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}