using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using System.Net.Http.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;

internal class GridResponseContentParser : IResponseConverter<GridResponse>
{
    public Task<GridResponse> Convert(HttpResponseMessage message) => message.Content.ReadFromJsonAsync<GridResponse>();
}