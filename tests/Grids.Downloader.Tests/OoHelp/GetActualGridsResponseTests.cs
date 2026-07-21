using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.OoHelp;

public class GetActualGridsResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "OoHelp";
    private readonly OohelpHttpService service = new(HttpClientFactory.Create());

    [Fact]
    public async Task GetActualGrids()
    {
        var grids = await service.GetActualGrids(new GetActualGridsRequest());

        Assert.NotEmpty(grids);
        Assert.Equal(9, grids.Count);

        Log($"{grids.Count} Providers received:");
        foreach (var prov in grids)
        {
            Log($"{prov.Provider.Name} ({prov.BoardsCount}) : {prov.Downloaded}");
        }

        await SaveResponse(grids, "GetActualGridsResponse.json");
    }
    [Fact]
    public async Task GetActualGridsWithWrongKey()
    {
        var request = new GetActualGridsRequest { Key = "key" };

        var ex = await Assert.ThrowsAsync<Exceptions.DownloaderException>(async () => await service.GetActualGrids(request));
        Assert.Equal(Exceptions.ErrorCode.ServerError, ex.ErrorCode);
    }
    [Fact]
    public async Task GetActualGridsWithActualKey()
    {
        var request = new GetActualGridsRequest { Key = Configuration.GetOoHelpApiKey() };

        Log(request.GetUri().ToString());
        var grids = await service.GetActualGrids(request);

        Assert.NotEmpty(grids);
        Assert.Equal(10, grids.Count);

        Log($"{grids.Count} Providers received:");
        foreach (var prov in grids)
        {
            Log($"{prov.Provider.Name} ({prov.BoardsCount}) : {prov.Downloaded}");
        }

        await SaveResponse(grids, "GetActualGridsResponseKeyAuth.json");
    }
    [Fact]
    public async Task GetActualGridsWithAuthentication()
    {
        var creds = Configuration.GetOoHelpCredentials();
        var request = new GetActualGridsRequest { Credential = creds };

        Log(request.GetUri().ToString());
        var grids = await service.GetActualGrids(request);

        Assert.NotEmpty(grids);
        Assert.Equal(10, grids.Count);

        Log($"{grids.Count} Providers received:");
        foreach (var prov in grids)
        {
            Log($"{prov.Provider.Name} ({prov.BoardsCount}) : {prov.Downloaded}");
        }

        await SaveResponse(grids, "GetActualGridsResponseCredAuth.json");
    }

    [Fact]
    public async Task GetActualGridsWithToken()
    {
        var creds = Configuration.GetOoHelpCredentials();
        var token = await service.GetLoginToken(creds);

        Assert.NotNull(token);

        var request = new GetActualGridsRequest { Token = token };

        Log(request.GetUri().ToString());
        var grids = await service.GetActualGrids(request);

        Assert.NotEmpty(grids);
        Assert.Equal(10, grids.Count);

        Log($"{grids.Count} Providers received:");
        foreach (var prov in grids)
        {
            Log($"{prov.Provider.Name} ({prov.BoardsCount}) : {prov.Downloaded}");
        }

        await SaveResponse(grids, "GetActualGridsResponseTokenAuth.json");
    }

    private async Task SaveResponse<TResponse>(TResponse response, string fileName)
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, fileName));

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, cancellationToken: TestContext.Current.CancellationToken);
    }
}
