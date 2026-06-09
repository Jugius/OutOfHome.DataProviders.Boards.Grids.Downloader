

using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Octagon;

public class ReadJsonToResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "Octagon";


    [Fact]
    public async Task ReadJsonToBoardsArray()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, "ResponseFormatted.json"));
        Assert.True(file.Exists);

        using var stream = file.OpenRead();
        var boards = await System.Text.Json.JsonSerializer.DeserializeAsync<OctagonBoard[]>(stream, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(boards);

        Log($"Boards count: {boards.Length}");
    }
}
