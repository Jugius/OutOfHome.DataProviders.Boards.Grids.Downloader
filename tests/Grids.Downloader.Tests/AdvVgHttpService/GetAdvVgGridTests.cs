using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.AdvVgHttpService;

public class GetAdvVgGridTests: TestBase
{
    private readonly Services.AdvVgHttpService service;
    public GetAdvVgGridTests(ITestOutputHelper output) : base(output)
    {
        var chromium = Configuration.GetChromiumFolder();
        var chromiumInitializer = new ChromiumInitializer(chromium);
        this.service = new Services.AdvVgHttpService(HttpClientFactory.Create(), chromiumInitializer);
    }
    [Fact]
    public async Task PerekhidAdvVg()
    {
        const string provider = "PerekhidAdvVg";
        var grid = await service.GetGrid(Entities.AdvVg.Common.Supplier.Perekhid);

        Assert.NotNull(grid);
        Assert.NotEmpty(grid.FileName);
        Assert.True(grid.Data.Length > 0);

        Log(grid.Uri);

        var path = Path.Combine(GetFilesFolder(provider), grid.FileName);
        await File.WriteAllBytesAsync(path, grid.Data, TestContext.Current.CancellationToken);
    }
    [Fact]
    public async Task SVOutdoorAdvVg()
    {
        const string provider = "SVOutdoorAdvVg";
        var grid = await service.GetGrid(Entities.AdvVg.Common.Supplier.SVOutdoor);

        Assert.NotNull(grid);
        Assert.NotEmpty(grid.FileName);
        Assert.True(grid.Data.Length > 0);

        Log(grid.Uri);

        var path = Path.Combine(GetFilesFolder(provider), grid.FileName);
        await File.WriteAllBytesAsync(path, grid.Data, TestContext.Current.CancellationToken);
    }
}
