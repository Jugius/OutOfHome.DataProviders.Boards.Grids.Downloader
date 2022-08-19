using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
public class ContentParser : Interfaces.IContentParser<List<BmaBoard>>
{
    public async Task<List<BmaBoard>> ParseContent(HttpResponseMessage message) =>
        await System.Text.Json.JsonSerializer.DeserializeAsync<List<BmaBoard>>(await message.Content.ReadAsStreamAsync());
}
