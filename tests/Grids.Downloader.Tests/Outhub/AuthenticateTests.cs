using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Outhub;

public class AuthenticateTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "Outhub";
    [Fact]
    public async Task GetAuthenticationCookies()
    {
        var creds = Configuration.GetOuthubCredentials();
        var chromium = Configuration.GetChromiumFolder();
        var chromiumInitializer = new ChromiumInitializer(chromium);
        var service = new OuthubHttpService(HttpClientFactory.Create(), chromiumInitializer);
        var cookies = await service.GetLoginCookies(creds);

        Assert.NotNull(cookies);
        Assert.NotEmpty(cookies);

        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, "Cookies.json"));

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, cookies, cancellationToken: TestContext.Current.CancellationToken);

        Log($"Cookies count: {cookies.Count}");
    }
}
