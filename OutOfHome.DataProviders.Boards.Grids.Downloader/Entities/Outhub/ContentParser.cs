using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

public class ContentParser : Interfaces.IContentParser<ResponseContent>
{
    public async Task<ResponseContent> ParseContent(HttpResponseMessage message) =>
        await System.Text.Json.JsonSerializer.DeserializeAsync<ResponseContent>(await message.Content.ReadAsStreamAsync());
}