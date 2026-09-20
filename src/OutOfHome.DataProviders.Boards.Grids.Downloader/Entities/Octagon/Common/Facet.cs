using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

public sealed record Facet(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name);
