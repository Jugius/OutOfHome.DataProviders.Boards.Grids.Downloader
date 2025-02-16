using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
public class AuthenticateRequest : IRequestPost
{
    private const string baseUri = "https://www.bma.ua/api/v1/auth/local/login";    
    public string UserName { get; set; }
    public string Password { get; set; }

    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent()
    {
        if (string.IsNullOrEmpty(UserName))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty UserName Field");

        if (string.IsNullOrEmpty(Password))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty Password Field");        

        return new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", UserName),
            new KeyValuePair<string, string>("password", Password)
        });
    }

    
}
