using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class AuthenticateRequest : IRequestPost
{
    private const string baseUri = "https://www.bma.ua/api/v1/auth/local/login";
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent()
    {
        if (string.IsNullOrEmpty(UserName))
            throw DownloaderException.InvalidRequest("Empty UserName Field");

        if (string.IsNullOrEmpty(Password))
            throw DownloaderException.InvalidRequest("Empty Password Field");

        return new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", UserName),
            new KeyValuePair<string, string>("password", Password)
        });
    }

    
}
