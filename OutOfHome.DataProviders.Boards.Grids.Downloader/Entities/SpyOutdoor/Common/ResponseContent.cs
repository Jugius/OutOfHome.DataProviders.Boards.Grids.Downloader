using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;
public class ResponseContent
{
    [JsonPropertyName("agencyProfile")]
    public AgencyProfile AgencyProfile { get; set; }

    [JsonPropertyName("regions")]
    public List<Region> Regions { get; set; }

    [JsonPropertyName("towns")]
    public List<Town> Towns { get; set; }

    [JsonPropertyName("constructTypes")]
    public List<ConstructType> ConstructTypes { get; set; }

    [JsonPropertyName("allConstructTypes")]
    public List<ConstructType> AllConstructTypes { get; set; }

    [JsonPropertyName("boardsTable")]
    public BoardsTable BoardsTable { get; set; }
}