using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Bigmedia;

public class AuthenticateTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "BMA";
    private readonly BigmediaHttpService service = new BigmediaHttpService(HttpClientFactory.Create());

    [Fact]
    public async Task GetAuthenticationResult()
    {
        var creds = Configuration.GetBMACredentials();

        var cookie = await service.GetLoginCookie(creds);

        Assert.NotNull(cookie);

        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, "Cookie.json"));

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, cookie, cancellationToken: TestContext.Current.CancellationToken);
    }

}
