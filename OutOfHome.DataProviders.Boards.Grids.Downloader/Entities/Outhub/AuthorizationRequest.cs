using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

public class AuthorizationRequest : IRequestPost
{
    private const string baseUri = "https://outhub.online/Account/LogOn";
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }


    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent()
    {
        if (string.IsNullOrEmpty(UserName))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty UserName Field");

        if (string.IsNullOrEmpty(Password))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty Password Field");        

        return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", UserName),
                new KeyValuePair<string, string>("Password", Password),
                new KeyValuePair<string, string>("RememberMe", RememberMe.ToString())
            });
    }    
}
