using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class ContentParser : Interfaces.IContentParser<List<Board>>
{
    public async Task<List<Board>> ParseContent(HttpResponseMessage message)
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        //var boards = await JsonSerializer.DeserializeAsync(await message.Content.ReadAsStreamAsync(), BigmediaJsonContext.Default.ListBmaBoard);
        var boards = await JsonSerializer.DeserializeAsync<InternalBmaBoard[]>(await message.Content.ReadAsStreamAsync(), jsonOptions);

        if (boards == null || boards.Length == 0)
            throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ServerError, $"Cервер вернул 0 записей.");

        List<Board> result = new List<Board>(boards.Length);

        foreach (var brd in boards)
        {
            if (brd.Lat.GetValueOrDefault(0) == 0 || brd.Lon.GetValueOrDefault(0) == 0)
                continue;

            brd.PhotoUrl = NormalizeUri(brd.PhotoUrl);
            brd.SchemaUrl = NormalizeUri(brd.SchemaUrl);
            result.Add(ToBoard(brd));
        }
        return result;
    }
    private static string NormalizeUri(string uri)
    {
        if (string.IsNullOrEmpty(uri))
            return null;

        //string newlink = uri.Replace("\"", "");

        return Uri.TryCreate(uri, UriKind.Absolute, out Uri u) ? u.ToString() : null;
    }

    private static Board ToBoard(InternalBmaBoard b) =>
        new Board
        {
            Address = b.Address,
            Angle = b.Angle,
            Grp = b.Grp,
            IdCatab = b.IdCatab,
            IdCity = b.IdCity,
            IdNetwork = b.IdNetwork,
            IdSize = b.IdSize,
            IdSupplier = b.IdSupplier,
            Lat = b.Lat,
            Lon = b.Lon,
            Light = b.Light,
            Ots = b.Ots,
            PhotoUrl = b.PhotoUrl,
            Price = b.Price,
            SchemaUrl = b.SchemaUrl,
            Sides = b.Sides,
            SupplierSidetype = b.SupplierSidetype,
        };
}
