using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;
using System.Text;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class AuthenticateRequest : IRequestPost
{
    private const string baseUri = "https://users.oohelp.net/api/user/login";
    public string Email { get; set; }
    public string Password { get; set; }
    public HttpContent GetContent()
    {
        if (string.IsNullOrEmpty(Email))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty Email");

        if (string.IsNullOrEmpty(Password))
            throw new DownloaderException(ErrorCode.InvalidRequest, "Empty Password");

        string json = System.Text.Json.JsonSerializer.Serialize(this);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public virtual Uri GetUri() => new Uri(baseUri);
}
