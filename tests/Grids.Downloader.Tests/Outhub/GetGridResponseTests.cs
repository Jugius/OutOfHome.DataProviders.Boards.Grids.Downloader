
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Outhub;

public class GetGridResponseTests : TestBase
{
    const string Provider = "Outhub";
    private readonly OuthubHttpService service;
    public GetGridResponseTests(ITestOutputHelper output) : base(output)
    {
        var chromium = Configuration.GetChromiumFolder();
        var chromiumInitializer = new ChromiumInitializer(chromium);
        this.service = new OuthubHttpService(HttpClientFactory.Create(), chromiumInitializer);
    }

    [Fact]
    public async Task GetResponseWithAuthentication()
    {
        var creds = Configuration.GetOuthubCredentials();
        var response = await service.GetGrid(creds);

        Assert.NotNull(response);

        await SaveResponse(response, "OuthubResponseWithAuth.json");
    }
    [Fact]
    public async Task GetResponseWithoutAuthentication()
    {
        var response = await service.GetGrid();

        Assert.NotNull(response);

        await SaveResponse(response, "OuthubResponse.json");
    }

    private async Task SaveResponse(Entities.Outhub.Common.ResponseContent response, string fileName)
    {
        var path = System.IO.Path.Combine(GetFilesFolder(Provider), fileName);
        using (var stream = System.IO.File.Create(path))
        {
            await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, defaultJsonOptions, TestContext.Current.CancellationToken);
        }
    }
}
