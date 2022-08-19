using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.GetGrid;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Downloader.Test.Octagon
{
    public class GetGridTest
    {
        private readonly ITestOutputHelper output;
        private OutOfHome.DataProviders.Boards.Grids.Downloader.OctagonHttpEngine httpService;
        public GetGridTest(ITestOutputHelper output)
        {
            this.output = output;
            this.httpService = new OutOfHome.DataProviders.Boards.Grids.Downloader.OctagonHttpEngine();
        }
        [Fact]
        public async Task DownloadResponse()
        {
            GetGridRequest request = new GetGridRequest
            {
                PeriodFrom = new DateOnly(2022, 8, 1),
                PeriodTo = new DateOnly(2022, 8, 31),
                CityId = "KIEV"
            };
            
            var s = request.GetUri();
            output.WriteLine("Request url: " + s.ToString());
            var response = await httpService.GetGrid.QueryAsync(request);

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
    }
}
