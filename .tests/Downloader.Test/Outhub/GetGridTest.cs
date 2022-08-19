using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.GetGrid;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Downloader.Test.Outhub;

public class GetGridTest
{
    private readonly ITestOutputHelper output;
    private OutOfHome.DataProviders.Boards.Grids.Downloader.OuthubHttpEngine outhubService;
    public GetGridTest(ITestOutputHelper output)
    {
        this.output = output;
        this.outhubService = new OutOfHome.DataProviders.Boards.Grids.Downloader.OuthubHttpEngine();
    }

    [Fact]
    public async Task DownloadResponse()
    {
        GetGridRequest request = new GetGridRequest();
        var s = request.GetUri();
        output.WriteLine("Request url: " + s.ToString());
        var response = await outhubService.GetGrid.QueryAsync(request);

        Assert.NotNull(response);
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        
        using (var stream = System.IO.File.Create(@"d:\MyFiles\GridDownloader Responses\tests\response.json"))
        {
            await System.Text.Json.JsonSerializer.SerializeAsync(stream, response, options);
        }
        
    }
    [Fact]
    public async Task OpenResponseFromFile()
    {
        GetGridResponse response;
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };

        using (var stream = System.IO.File.OpenRead(@"d:\MyFiles\GridDownloader Responses\tests\response.json"))
        {
            response = await JsonSerializer.DeserializeAsync<GetGridResponse>(stream, options);
        }
        Assert.True(response.Result.Boards.Length > 0);
        output.WriteLine(response.Result.Boards[0]);
        output.WriteLine(GetAddress(response.Result.Boards[1]));

    }
    private static string GetAddress(string board)
    {
        ReadOnlySpan<string> splitted = board.Split(';');
        return splitted[15];
    }
}
