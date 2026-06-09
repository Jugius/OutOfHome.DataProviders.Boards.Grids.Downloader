using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;

internal class ContentParser : IResponseConverter<OctagonBoard[]>
{
    public async Task<OctagonBoard[]> Convert(HttpResponseMessage message)
    {
        var rowStr = await message.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(rowStr))
            throw DownloaderException.ServerError("Сервер вернул нулевой результат.");

        try
		{
            var boards = System.Text.Json.JsonSerializer.Deserialize<OctagonBoard[]>(rowStr);
            return boards is null ? throw DownloaderException.ServerError("Сервер вернул нулевой результат.") : boards;
        }
        catch (Exception ex)
		{
            throw DownloaderException.ContentParsingError("Ошибка десериализации OctagonBoard[]", ex);
		}

    }
}