using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Octagon;

public class GridResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "Octagon";
    const string FileName = "GridResponse";
    private readonly OctagonHttpService service = new(HttpClientFactory.Create());

    [Fact]
    public async Task DownloadResponse()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, $"{FileName}.json"));


        var response = await service.GetGrid();

        Assert.NotNull(response);

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, defaultJsonOptions, cancellationToken: TestContext.Current.CancellationToken);

        Log($"Boards count: {response.Boards.Length}");
    }
    [Fact]
    public async Task DownloadRowResponseAsString()
    {
        var folder = GetFilesFolder(Provider);
        var file = Path.Combine(folder, $"{FileName}.txt");

        var row = await service.GetResponseStringAsync();
        Assert.False(string.IsNullOrEmpty(row));

        await File.WriteAllTextAsync(file, row, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task ResponseDoesNotContainsBoardsWithUnknownOccupancies()
    {
        var knownChars = new HashSet<char> { 'A', 'S', 'P', 'X' };
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, $"{FileName}.json"));

        using var stream = file.OpenRead();
        var response = await System.Text.Json.JsonSerializer.DeserializeAsync<GridResponse>(stream, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(response);
        Assert.True(response.Boards.Length > 0);

        Log($"Boards count: {response.Boards?.Length}");
        Dictionary<char, string> variants = new Dictionary<char, string>();

        foreach (var board in response.Boards!)
        {
            foreach (var o in board.Availability)
            {
                if (variants.ContainsKey(o)) continue;
                variants[o] = board.DisplayCode;
            }
        }

        Log($"Boards Occupancies: {variants.Count}");
        foreach (var item in variants)
        {
            Log($"'{item.Key}' : '{item.Value}'");
        }
        var unknownChars = variants.Keys.Except(knownChars).ToArray();
        Assert.Empty(unknownChars);
    }
    [Fact]
    public async Task AllBoardsAvailabilityLengthEqualsPeriodsCount()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, $"{FileName}.json"));

        using var stream = file.OpenRead();
        var response = await System.Text.Json.JsonSerializer.DeserializeAsync<GridResponse>(stream, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(response);
        Assert.True(response.Boards.Length > 0);

        Log($"Boards count: {response.Boards?.Length}");
        Assert.All(response.Boards, board => Assert.Equal(response.Periods.Length, board.Availability.Length));
    }

    [Fact]
    public async Task ReadJsonToBoardsArray()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, $"{FileName}.json"));
        Assert.True(file.Exists);

        using var stream = file.OpenRead();
        var response = await System.Text.Json.JsonSerializer.DeserializeAsync<GridResponse>(stream, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(response);

        Log($"Boards count: {response.Boards.Length}");
    }
}
