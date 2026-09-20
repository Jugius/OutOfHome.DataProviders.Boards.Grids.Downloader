using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed record GridResponse(
    [property: JsonPropertyName("periods")] DateRange[] Periods,
    [property: JsonPropertyName("faces")] OctagonBoard[] Boards);
