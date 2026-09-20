using System.Net.Http.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
internal sealed record GetGridRequest : Interfaces.IRequestPost
{
    private const string baseUri = "https://sales.octagon.com.ua/api/v1/public/faces/search";
    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent() => JsonContent.Create(new { });
}
