
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;
using System.Net;

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

    [Fact]
    public async Task ConvertResponseToLines()
    {
        var file = new FileInfo(Path.Combine(GetFilesFolder(Provider), "OuthubResponseWithAuth.json"));

        using var fs = file.OpenRead();
        var response = await System.Text.Json.JsonSerializer.DeserializeAsync<Entities.Outhub.Common.ResponseContent>(fs, defaultJsonOptions, TestContext.Current.CancellationToken);
        
        var path = Path.Combine(GetFilesFolder(Provider), "OuthubResponseWithAuthLines.txt");
        await File.WriteAllLinesAsync(path, response.Boards, TestContext.Current.CancellationToken);

        Log($"Boards count: {response.Boards.Length}");
    }
}
