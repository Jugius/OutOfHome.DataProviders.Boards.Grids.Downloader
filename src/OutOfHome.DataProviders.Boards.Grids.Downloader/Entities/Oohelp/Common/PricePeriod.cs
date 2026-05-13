using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
public class PricePeriod
{
    [JsonPropertyName("s")]
    public DateOnly Start { get; set; }


    [JsonPropertyName("e")]
    public DateOnly End { get; set; }

    [JsonPropertyName("val")]
    public int Value { get; set; }
}
