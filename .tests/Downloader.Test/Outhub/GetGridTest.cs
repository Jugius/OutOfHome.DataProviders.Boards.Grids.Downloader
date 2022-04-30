using OutOfHome.DataProviders.Boards.Grids.Downloader;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Downloader.Test.Outhub
{
    public class GetGridTest
    {
        private readonly ITestOutputHelper output;
        public GetGridTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task DownloadResponse()
        {
            GetGridRequest request = new GetGridRequest();
            var s = request.GetUri();
            output.WriteLine(s.ToString());
            var response = await OutOfHome.DataProviders.Boards.Grids.Downloader.Outhub.GetGrid.QueryAsync(request);

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
            Assert.True(response.Boards.Length > 0);
            output.WriteLine(response.Boards[0]);
            output.WriteLine(GetAddress(response.Boards[1]));

        }
        private static string GetAddress(string board)
        {
            ReadOnlySpan<string> splitted = board.Split(';');
            return splitted[15];
        }
    }
}
