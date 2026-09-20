using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed record OctagonBoard(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("formatName")] string FormatName,
    [property: JsonPropertyName("formatGroupName")] string FormatGroupName,
    [property: JsonPropertyName("regionName")] string RegionName,
    [property: JsonPropertyName("cityName")] string CityName,
    [property: JsonPropertyName("location")] string Street,
    [property: JsonPropertyName("latitude")] double Latitude,
    [property: JsonPropertyName("longitude")] double Longitude,
    [property: JsonPropertyName("bearing")] short? Bearing,
    [property: JsonPropertyName("structureCode")] string StructureCode,
    [property: JsonPropertyName("displayCode")] string DisplayCode,
    [property: JsonPropertyName("side")] string Side,
    [property: JsonPropertyName("placementPrice")] float PlacementPrice,
    [property: JsonPropertyName("grp")] float Grp,
    [property: JsonPropertyName("ots")] float Ots,
    [property: JsonPropertyName("grade")] int Grade,
    [property: JsonPropertyName("isIlluminated")] bool IsIlluminated,
    [property: JsonPropertyName("placementRequirementsUrl")] string? PlacementRequirementsUrl,
    [property: JsonPropertyName("availability")] string Availability,
    [property: JsonPropertyName("availabilitySummary")] string AvailabilitySummary);