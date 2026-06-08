using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

internal class GetGridResponseContentConverter : Interfaces.IResponseConverter<ResponseContent>
{
    public async Task<ResponseContent> Convert(HttpResponseMessage message)
    { 
        var content = await System.Text.Json.JsonSerializer.DeserializeAsync<ResponseContent>(await message.Content.ReadAsStreamAsync());
        if (content!.ServerStatus < 0)
            throw Exceptions.DownloaderException.ServerError($"Cервер вернул статус: {content.ServerStatus}");
        return content;
    }
        
}