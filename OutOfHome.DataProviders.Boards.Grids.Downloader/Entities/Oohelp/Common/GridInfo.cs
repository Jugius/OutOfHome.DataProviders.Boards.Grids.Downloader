using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
public class GridInfo
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("provider")]
    public ProviderInfo Provider { get; set; }


    [JsonPropertyName("downloaded")]
    public DateTime Downloaded { get; set; }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("language")]
    public Language Language { get; set; }


    [JsonPropertyName("count")]
    public int BoardsCount { get; set; }
}
