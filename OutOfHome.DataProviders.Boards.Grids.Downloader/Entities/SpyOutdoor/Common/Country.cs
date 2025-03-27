using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class Country
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("iso")]
    public string Iso { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("lat")]
    public int Lat { get; set; }

    [JsonPropertyName("lon")]
    public int Lon { get; set; }

    [JsonPropertyName("boxEastLongitude")]
    public int BoxEastLongitude { get; set; }

    [JsonPropertyName("boxNorthLatitude")]
    public int BoxNorthLatitude { get; set; }

    [JsonPropertyName("boxSouthLatitude")]
    public int BoxSouthLatitude { get; set; }

    [JsonPropertyName("boxWestLongitude")]
    public int BoxWestLongitude { get; set; }
}
