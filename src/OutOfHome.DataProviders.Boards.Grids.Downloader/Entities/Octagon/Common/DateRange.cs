using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed record DateRange(
    [property: JsonPropertyName("startsOn")] DateOnly Start,
    [property: JsonPropertyName("endsOn")] DateOnly End);
