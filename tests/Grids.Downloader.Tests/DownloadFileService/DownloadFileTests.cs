namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.DownloadFileService;

public class DownloadFileTests(ITestOutputHelper output) : TestBase(output)
{
    private readonly Services.DownloadFileHttpService downloadFileService = new(HttpClientFactory.Create());

    [Fact]
    public async Task Megaboard()
    {
        const string Provider = "Megaboard";
        var query = Entities.Common.DownloadFileRequest.Megaboard;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);        
    }

    [Fact]
    public async Task Ollan()
    {
        const string Provider = "Ollan";
        var query = Entities.Common.DownloadFileRequest.Ollan;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);
    }
    [Fact]
    public async Task PozitivBC()
    {
        const string Provider = "PozitivBC";
        var query = Entities.Common.DownloadFileRequest.PozitivBC;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);        
    }
    [Fact]
    public async Task RoyalTrend()
    {
        const string Provider = "RoyalTrend";
        var query = Entities.Common.DownloadFileRequest.RoyalTrend;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);
    }
    [Fact]
    public async Task Kent()
    {
        const string Provider = "Kent";
        var query = Entities.Common.DownloadFileRequest.Kent;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);        
    }
    [Fact]
    public async Task Perekhid()
    {
        const string Provider = "Perekhid";
        var query = Entities.Common.DownloadFileRequest.Perekhid;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);
    }
    [Fact]
    public async Task SVOutdoor()
    {
        const string Provider = "SVOutdoor";
        var query = Entities.Common.DownloadFileRequest.SVOutdoor;

        var file = await downloadFileService.DownloadFile(query, TestContext.Current.CancellationToken);
        Assert.NotNull(file);
        Assert.NotNull(file.Data);

        Log(file.FileName);
        Log(file.FileType.ToString());

        var folder = GetFilesFolder(Provider);
        string path = Path.Combine(folder, file.FileName);
        await File.WriteAllBytesAsync(path, file.Data, TestContext.Current.CancellationToken);
    }
}
