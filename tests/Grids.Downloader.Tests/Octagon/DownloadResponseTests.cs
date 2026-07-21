
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Octagon;

public class DownloadResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "Octagon";
    private readonly OctagonHttpService service = new(HttpClientFactory.Create());

    [Fact]
    public async Task DownloadResponse()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, "Response.json"));


        var boards = await service.GetGrid();

        Assert.NotNull(boards);

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, boards, cancellationToken: TestContext.Current.CancellationToken);

        Log($"Boards count: {boards.Length}");
    }
    [Fact]
    public async Task DownloadRowResponseString()
    {
        var folder = GetFilesFolder(Provider);
        var file = Path.Combine(folder, "Response.txt");

        var row = await service.GetResponseStringAsync();
        Assert.False(string.IsNullOrEmpty(row));

        await File.WriteAllTextAsync(file, row, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task ReadRowResponseString()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, "Response.txt"));

        using var stream = file.OpenRead();
        var boards = await System.Text.Json.JsonSerializer.DeserializeAsync<OctagonBoard[]>(stream, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(boards);
        Assert.True(boards.Length > 0);

        foreach (var board in boards)
        {
            foreach (var o in board.Occupancies)
            {
                if (o.Status == 0)
                    Log(board.ShortCode);

            }
        }

        Log($"Boards count: {boards.Length}");

        Assert.All(boards.SelectMany(b => b.Occupancies), o => Assert.True(o.Status is OccupancyStatus.Booked or OccupancyStatus.Reserved or OccupancyStatus.Free));
    }
}
