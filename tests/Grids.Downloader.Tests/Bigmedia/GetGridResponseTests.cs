using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Bigmedia;

public class GetGridResponseTests(ITestOutputHelper output) : TestBase(output)
{
    const string Provider = "BMA";
    private readonly BigmediaHttpService service = new BigmediaHttpService(HttpClientFactory.Create());

    [Fact]
    public async Task GetResponseWithAuthentication()
    {
        var creds = Configuration.GetBMACredentials();
        var request = new GetGridRequest
        {
            Credential = creds,
        };
        var boards = await service.GetGrid(request);

        Assert.NotEmpty(boards);

        var folder = GetFilesFolder(Provider);
        var file = new FileInfo(Path.Combine(folder, "BmaResponse.json"));

        using var stream = file.Create();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, boards, cancellationToken: TestContext.Current.CancellationToken);
    }
}
