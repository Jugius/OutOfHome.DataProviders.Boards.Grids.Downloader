using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
using System.Text.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
internal class GetActualGridsContentParser : Interfaces.IContentParser<List<GridInfo>>
{
    public async Task<List<GridInfo>> ParseContent(HttpResponseMessage message)
    {
        await using var stream = await message.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<List<GridInfo>>(stream);
    }
}
