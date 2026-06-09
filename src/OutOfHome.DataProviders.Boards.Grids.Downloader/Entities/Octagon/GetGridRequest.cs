using System.Text;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
internal class GetGridRequest : Interfaces.IRequestPost
{
    private const string baseUri = "https://sales.octagon.com.ua/api/v1/media-faces/search";
    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent()
    {
        var json = """
        {
            "actors": ["A82ED5C4C3B4"]
        }
        """;

        return new StringContent(
            json,
            Encoding.UTF8,
            "application/json");
    }   
    
}
