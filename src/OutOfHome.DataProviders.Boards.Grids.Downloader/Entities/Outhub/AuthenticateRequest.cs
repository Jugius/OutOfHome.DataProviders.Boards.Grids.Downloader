
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

public class AuthenticateRequest
{
    internal string Uri { get; } = "https://booking.outhub.online/Account/LogOn";
    public string UserName { get; set; }
    public string Password { get; set; }
}
