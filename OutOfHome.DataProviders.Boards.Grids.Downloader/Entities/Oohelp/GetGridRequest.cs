using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common.Enums;
using System.Text;
using System.Text.Json.Serialization;


namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetGridRequest : IRequestPost
{
    private readonly Uri _url = new Uri("https://boards.oohelp.net/api/grids");
    //private readonly Uri _url = new Uri("https://localhost:7250/api/grids");

    [JsonPropertyName("bma")]
    public BmaExportMode Bma { get; set; }


    [JsonPropertyName("outhub")]
    public OuthubExportMode Outhub { get; set; }


    [JsonPropertyName("octagon")]
    public OctagonExportMode Octagon { get; set; }


    [JsonPropertyName("perekhid")]
    public PerekhidExportMode Perekhid { get; set; }


    [JsonPropertyName("output")]
    public OutputFormat OutputFormat { get; } = OutputFormat.zip;


    [JsonPropertyName("remove_duplicates")]
    public bool RemoveDuplicates { get; set; }


    [JsonPropertyName("key")]
    public string Key { get; set; }

    public HttpContent GetContent()
    {
        string json = System.Text.Json.JsonSerializer.Serialize(this);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        return httpContent;
    }

    public Uri GetUri() => _url;
}
