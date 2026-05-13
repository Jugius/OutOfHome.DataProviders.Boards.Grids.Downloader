
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
public class OccupationPeriod
{
    [JsonPropertyName("s")]
    public DateOnly Start { get; set; }


    [JsonPropertyName("e")]
    public DateOnly End { get; set; }

    [JsonPropertyName("c")]
    public int Condition { get; set; }

}
