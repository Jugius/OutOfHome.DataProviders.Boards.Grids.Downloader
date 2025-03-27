using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class Agency
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("titleUa")]
    public string TitleUa { get; set; }

    [JsonPropertyName("colorF")]
    public string ColorF { get; set; }

    [JsonPropertyName("colorR")]
    public string ColorR { get; set; }

    [JsonPropertyName("colorP")]
    public string ColorP { get; set; }

    [JsonPropertyName("periodStart")]
    public string PeriodStart { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("country")]
    public Country Country { get; set; }
}
