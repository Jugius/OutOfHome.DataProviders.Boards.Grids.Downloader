using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.AdvVgHttpService;

public class GetAdvVgGridTests(ITestOutputHelper output) : TestBase(output)
{
    [Fact]
    public async Task PerekhidAdvVg()
    {
        const string provider = "PerekhidAdvVg";
        var service = new PerekhidHttpService();
        var grid = await service.GetGrid();

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
        var service = new SVOutdoorHttpService();
        var grid = await service.GetGrid();

        Assert.NotNull(grid);
        Assert.NotEmpty(grid.FileName);
        Assert.True(grid.Data.Length > 0);

        Log(grid.Uri);

        var path = Path.Combine(GetFilesFolder(provider), grid.FileName);
        await File.WriteAllBytesAsync(path, grid.Data, TestContext.Current.CancellationToken);
    }
}
