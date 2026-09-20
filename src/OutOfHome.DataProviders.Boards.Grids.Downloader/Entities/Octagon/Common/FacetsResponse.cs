using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed record FacetsResponse(
    [property: JsonPropertyName("formats")] Facet[] Formats,
    [property: JsonPropertyName("cities")] Facet[] Cities);
