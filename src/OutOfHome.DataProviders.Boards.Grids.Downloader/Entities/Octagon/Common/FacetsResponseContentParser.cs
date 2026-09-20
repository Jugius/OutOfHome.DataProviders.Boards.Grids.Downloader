using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using System.Net.Http.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

internal sealed class FacetsResponseContentParser : IResponseConverter<FacetsResponse>
{
    public Task<FacetsResponse> Convert(HttpResponseMessage message) => message.Content.ReadFromJsonAsync<FacetsResponse>();
}
