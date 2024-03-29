﻿using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common.Enums;
using OutOfHome.DataProviders.Boards.Grids.Downloader.JsonConverters;
using System.Text;
using System.Text.Json.Serialization;


namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetGridRequest : IRequestPost
{
    [JsonPropertyName("bma")]
    public BmaExportMode? Bma { get; set; }


    [JsonPropertyName("outhub")]
    public OuthubExportMode? Outhub { get; set; }


    [JsonPropertyName("octagon")]
    public OctagonExportMode? Octagon { get; set; }


    [JsonPropertyName("perekhid")]
    public PerekhidExportMode? Perekhid { get; set; }


    [JsonPropertyName("svoutdoor")]
    public SVOutdoorExportMode? SVOutdoor { get; set; }


    [JsonPropertyName("output")]
    public OutputFormat OutputFormat { get; } = OutputFormat.zip;


    [JsonPropertyName("grids_date")]
    [JsonConverter(typeof(JsonDateOnlyStringConverter))]
    public DateOnly? GridsDownloadedDate { get; set; }


    [JsonPropertyName("remove_duplicates")]
    public bool RemoveDuplicates { get; set; }


    [JsonPropertyName("remove_empty_locations")]
    public bool RemoveEmptyLocations { get; set; }

    
    [JsonPropertyName("remove_inactive")]
    public bool RemoveInactiveRecords { get; set; }


    [JsonPropertyName("key")]
    public string Key { get; set; }

    public HttpContent GetContent()
    {
        string json = System.Text.Json.JsonSerializer.Serialize(this);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        return httpContent;
    }

    public Uri GetUri() => new Uri(OohelpRequestBase.URL);
}
