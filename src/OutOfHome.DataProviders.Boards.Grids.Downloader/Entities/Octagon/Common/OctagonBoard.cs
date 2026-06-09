using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed class OctagonBoard
{
    [JsonPropertyName("hashId")] public string HashId { get; set; } = null!;
    [JsonPropertyName("Id")] public string Id { get; set; } = null!;
    [JsonPropertyName("mediaFormatId")] public string FormatId { get; set; } = null!;
    [JsonPropertyName("mediaFormatName")] public string FormatName { get; set; } = null!;
    [JsonPropertyName("mediaCategoryId")] public Guid CategoryId { get; set; }
    [JsonPropertyName("mediaCategoryName")] public string CategoryName { get; set; } = null!;
    [JsonPropertyName("actorId")] public string ActorId { get; set; } = null!;
    [JsonPropertyName("actorName")] public string ActorName { get; set; } = null!;
    [JsonPropertyName("actorCode")] public string? ActorCode { get; set; }
    [JsonPropertyName("geoCityId")] public string CityId { get; set; } = null!;
    [JsonPropertyName("geoCityName")] public string CityName { get; set; } = null!;
    [JsonPropertyName("geoRegionId")] public string RegionId { get; set; } = null!;
    [JsonPropertyName("geoRegionName")] public string RegionName { get; set; } = null!;
    [JsonPropertyName("geoLatitude")] public double Latitude { get; set; }
    [JsonPropertyName("geoLongitude")] public double Longitude { get; set; }
    [JsonPropertyName("geoBearing")] public short? Bearing { get; set; }
    [JsonPropertyName("geoAddress")] public string Address { get; set; } = null!;
    [JsonPropertyName("constructionCode")] public string ConstructionCode { get; set; } = null!;
    [JsonPropertyName("shortCode")] public string ShortCode { get; set; } = null!;
    [JsonPropertyName("facade")] public string Side { get; set; } = null!;
    [JsonPropertyName("cost")] public int Price { get; set; }
    [JsonPropertyName("price2")] public int Price2 { get; set; }
    [JsonPropertyName("grp")] public float Grp { get; set; }
    [JsonPropertyName("ots")] public float Ots { get; set; }
    [JsonPropertyName("isReady")] public bool IsReady { get; set; }
    [JsonPropertyName("isIlluminated")] public bool IsIlluminated { get; set; }
    [JsonPropertyName("isAssigned")] public bool IsAssigned { get; set; }
    [JsonPropertyName("isDigit")] public bool IsDigit { get; set; }
    [JsonPropertyName("occupancies")] public Occupancy[] Occupancies { get; set; } = [];

    // [JsonPropertyName("occupancySummaryStatus")] public int OccupancySummaryStatus { get; set; }
    // [JsonPropertyName("favorite")] public object favorite { get; set; }
}