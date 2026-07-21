using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.OoHelp;

public class GetGridResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "OoHelp";
    private readonly OohelpHttpService service = new(HttpClientFactory.Create());
    [Fact]
    public async Task GetGrid()
    {
        GetGridRequest gridRequest = new() 
        {
            RemoveDuplicates = true,
            RemoveEmptyLocations = true,
            RemoveInactiveRecords = false,
        };
        Log("Request url: " + gridRequest.GetUri().ToString());

        var gridResponse = await this.service.GetGrid(gridRequest);

        Assert.NotNull(gridResponse);
        Assert.True(gridResponse.Boards.Length > 0);

        Log("Boards count: " + gridResponse.Boards.Length);
        Log("Lang: " + gridResponse.Language);
        Log("Date: " + gridResponse.Downloaded.ToString("MM.dd HH:mm:ss"));

        await SaveResponse(gridResponse, "GetGridResponse.json");
    }
    [Fact]
    public async Task GetGridWithActualKey()
    {
        GetGridRequest gridRequest = new()
        {
            RemoveDuplicates = true,
            RemoveEmptyLocations = true,
            RemoveInactiveRecords = false,
            Key = Configuration.GetOoHelpApiKey()
        };
        Log("Request url: " + gridRequest.GetUri().ToString());

        var gridResponse = await this.service.GetGrid(gridRequest);

        Assert.NotNull(gridResponse);
        Assert.True(gridResponse.Boards.Length > 0);

        Log("Boards count: " + gridResponse.Boards.Length);
        Log("Lang: " + gridResponse.Language);
        Log("Date: " + gridResponse.Downloaded.ToString("MM.dd HH:mm:ss"));

        await SaveResponse(gridResponse, "GetGridResponseKeyAuth.json");
    }

    [Fact]
    public async Task GetGridWithAuthentication()
    {
        var creds = Configuration.GetOoHelpCredentials();

        GetGridRequest gridRequest = new()
        {
            RemoveDuplicates = true,
            RemoveEmptyLocations = true,
            RemoveInactiveRecords = false,
            Credential = creds
        };
        Log("Request url: " + gridRequest.GetUri().ToString());

        var gridResponse = await this.service.GetGrid(gridRequest);

        Assert.NotNull(gridResponse);
        Assert.True(gridResponse.Boards.Length > 0);

        Log("Boards count: " + gridResponse.Boards.Length);
        Log("Lang: " + gridResponse.Language);
        Log("Date: " + gridResponse.Downloaded.ToString("MM.dd HH:mm:ss"));

        await SaveResponse(gridResponse, "GetGridResponseCredAuth.json");
    }
    [Fact]
    public async Task GetGridWithToken()
    {
        var creds = Configuration.GetOoHelpCredentials();
        var token = await service.GetLoginToken(creds);

        GetGridRequest gridRequest = new()
        {
            RemoveDuplicates = true,
            RemoveEmptyLocations = true,
            RemoveInactiveRecords = false,
            Token = token
        };
        Log("Request url: " + gridRequest.GetUri().ToString());

        var gridResponse = await this.service.GetGrid(gridRequest);

        Assert.NotNull(gridResponse);
        Assert.True(gridResponse.Boards.Length > 0);

        Log("Boards count: " + gridResponse.Boards.Length);
        Log("Lang: " + gridResponse.Language);
        Log("Date: " + gridResponse.Downloaded.ToString("MM.dd HH:mm:ss"));

        await SaveResponse(gridResponse, "GetGridResponseTokenAuth.json");
    }

    private async Task SaveResponse<TResponse>(TResponse response, string fileName)
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, fileName));

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, cancellationToken: TestContext.Current.CancellationToken);
    }
}
