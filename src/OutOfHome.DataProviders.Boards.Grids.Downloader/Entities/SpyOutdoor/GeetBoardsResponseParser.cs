
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;
using System.Net.Http.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
internal class GeetBoardsResponseParser : Interfaces.IResponseConverter<Common.BoardsTable>
{
    public async Task<BoardsTable> Convert(HttpResponseMessage message) => await message.Content.ReadFromJsonAsync<BoardsTable>();
}
