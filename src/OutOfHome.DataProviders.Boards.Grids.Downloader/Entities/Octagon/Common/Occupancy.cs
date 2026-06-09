

using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed class Occupancy
{
    [JsonPropertyName("periodAt")] public DateOnly PeriodAt { get; set; }
    [JsonPropertyName("status")] public OccupancyStatus Status { get; set; }
}
