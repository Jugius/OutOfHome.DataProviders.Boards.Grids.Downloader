using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.OoHelp;

public class AuthenticateTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "OoHelp";
    private readonly OohelpHttpService service = new(HttpClientFactory.Create());

    [Fact]
    public async Task GetAuthenticationToken()
    {
        var creds = Configuration.GetOoHelpCredentials();
        var token = await service.GetLoginToken(creds);

        Assert.NotEmpty(token);
        Log(token);
    }
}
