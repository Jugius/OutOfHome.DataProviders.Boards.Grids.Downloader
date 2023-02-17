using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
public class ResponseContent
{
    [JsonPropertyName("boards")]
    public Board[] Boards { get; set; }


    [JsonPropertyName("downloaded")]
    public DateTime Downloaded { get; set; }


    [JsonPropertyName("language")]
    public Language Language { get; set; }
}
