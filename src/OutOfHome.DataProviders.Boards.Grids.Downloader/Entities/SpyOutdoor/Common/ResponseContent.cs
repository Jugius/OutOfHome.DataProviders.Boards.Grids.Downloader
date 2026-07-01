using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;
public sealed class ResponseContent
{
    [JsonPropertyName("agencyProfile")]
    public AgencyProfile AgencyProfile { get; set; } = default!;

    [JsonPropertyName("regions")]
    public IReadOnlyList<Region> Regions { get; set; } = default!;

    [JsonPropertyName("towns")]
    public List<Town> Towns { get; set; } = default!;

    [JsonPropertyName("constructTypes")]
    public List<ConstructType> ConstructTypes { get; set; } = default!;

    [JsonPropertyName("allConstructTypes")]
    public List<ConstructType>? AllConstructTypes { get; set; }
}