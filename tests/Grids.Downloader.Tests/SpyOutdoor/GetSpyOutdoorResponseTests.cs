using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.SpyOutdoor;

public class GetSpyOutdoorResponseTests(ITestOutputHelper output) : TestBase(output)
{
    private const string provider = "SpyOutdoor";
    private readonly Services.SpyOutdoorHttpService spyOutdoorService = new(HttpClientFactory.Create());
    
    [Theory]
    [InlineData(Supplier.Igrok)]
    [InlineData(Supplier.Brok)]
    [InlineData(Supplier.Unicom)]
    public async Task DownloadResponseAndSaveAsJsonFile(Supplier supplier)
    {
        var response = await spyOutdoorService.GetGrid(new GetGridRequest { Supplier = supplier });
        Assert.NotNull(response);
        var path = Path.Combine(GetFilesFolder(provider), $"{supplier}_GetGridResponse.json");
        using var stream = File.Create(path);
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, defaultJsonOptions, TestContext.Current.CancellationToken);

        var start = response.AgencyProfile.Agency.PeriodStart;
        var regions = response.Regions.Where(t => t.BoardCount > 0).ToList();
        var towns = response.Towns.Where(t => t.BoardCount > 0).ToList();
        var constructTypes = response.ConstructTypes.Where(t => t.BoardCount > 0).ToList();
        var totalCount = towns.Sum(t => t.BoardCount);
        var boards = new List<Downloader.Entities.SpyOutdoor.Common.Board>(totalCount);

        Log($"Total count: {totalCount}");

        foreach (var town in towns)
        {
            var townBoards = await GetBoardsByTown(supplier, town);
            boards.AddRange(townBoards);
        }

        Log($"Boards count: {boards.Count}");

        Assert.Equal(totalCount, boards.Count);

        var grid = new
        {
            Provider = supplier.ToString(),
            Downloaded = DateTime.Now,

            Start = start,
            Regions = regions,
            Towns = towns,
            ConstructTypes = constructTypes,

            Boards = boards,
            BoardsCount = totalCount
        };

        path = Path.Combine(GetFilesFolder(provider), $"{supplier}_Grid.json");
        using var outstream = File.Create(path);
        await System.Text.Json.JsonSerializer.SerializeAsync(outstream, grid, defaultJsonOptions, TestContext.Current.CancellationToken);
    } 
    private async Task<IReadOnlyList<Board>> GetBoardsByTown(Supplier supplier, Town town)
    {
        const int pageSize = 400;

        var boards = new List<Board>(town.BoardCount);

        for (int offset = 0; offset < town.BoardCount; offset += pageSize)
        {
            
            var request = new Downloader.Entities.SpyOutdoor.GetBoardsRequest(
                supplier,
                town.Id,
                offset,
                Math.Min(pageSize, town.BoardCount - offset));

            var table = await spyOutdoorService.GetBoards(request);
            boards.AddRange(table.Boards);
        }
        return boards;
    }
}

