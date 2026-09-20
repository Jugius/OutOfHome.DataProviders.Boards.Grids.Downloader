using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Octagon;

public class FacetsResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "Octagon";
    const string FileName = "FacetsResponse";
    private readonly OctagonHttpService service = new(HttpClientFactory.Create());
    [Fact]
    public async Task DownloadResponse()
    {
        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, $"{FileName}.json"));


        var response = await service.GetFacets();

        Assert.NotNull(response);

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, defaultJsonOptions, cancellationToken: TestContext.Current.CancellationToken);

        Log($"Formats count: {response.Formats.Length}");
        Log($"Regions count: {response.Cities.Length}");
    }
}
