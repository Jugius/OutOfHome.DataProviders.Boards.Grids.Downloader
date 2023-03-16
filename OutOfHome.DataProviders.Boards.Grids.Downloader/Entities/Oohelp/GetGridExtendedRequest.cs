using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common.Enums;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;
using System.Text;
using System.Text.Json.Serialization;


namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetGridExtendedRequest : IRequestPost
{
    //private const string baseUri = "https://localhost:7244/api/grids/download";
    private const string baseUri = "https://boards.oohelp.net/api/grids/download";

    [JsonIgnore]
    public string ApiKey { get; set; }


    [JsonPropertyName("bma")]
    public BmaExportMode Bma { get; set; }


    [JsonPropertyName("outhub")]
    public OuthubExportMode Outhub { get; set; }


    [JsonPropertyName("octagon")]
    public OctagonExportMode Octagon { get; set; }

    public HttpContent GetContent()
    {
        string json = System.Text.Json.JsonSerializer.Serialize(this);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        return httpContent;
    }

    public Uri GetUri()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty Api key Field");        
        return new Uri($"{baseUri}/{ApiKey}");
    }
}
