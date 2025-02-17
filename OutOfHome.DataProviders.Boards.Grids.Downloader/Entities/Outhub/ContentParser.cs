using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

internal class ContentParser : Interfaces.IResponseConverter<ResponseContent>
{
    public async Task<ResponseContent> Convert(HttpResponseMessage message)
    { 
        var content = await System.Text.Json.JsonSerializer.DeserializeAsync<ResponseContent>(await message.Content.ReadAsStreamAsync());
        if (content.ServerStatus < 0)
            throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ServerError, $"Cервер вернул статус: {content.ServerStatus}");
        return content;
    }
        
}