using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class GetGridResponseContentConverter : Interfaces.IResponseConverter<List<Board>>
{
    public async Task<List<Board>> Convert(HttpResponseMessage message)
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        Board[] boards;

        using (var readingStream = await message.Content.ReadAsStreamAsync())
        {
            try
            {
                boards = await JsonSerializer.DeserializeAsync<Board[]>(readingStream, jsonOptions);
                
                if (boards == null || boards.Length == 0)
                    throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ServerError, $"Cервер вернул 0 записей.");
            }
            catch (JsonException)
            {                
                readingStream.Position = 0;
                var internalBoards = await JsonSerializer.DeserializeAsync<InternalBmaBoard[]>(await message.Content.ReadAsStreamAsync(), jsonOptions);
                boards = FilterAndConvertToBoards(internalBoards).ToArray();
            }
        }    

        foreach (var brd in boards)
        {
            brd.PhotoUrl = NormalizeUri(brd.PhotoUrl);
            brd.SchemaUrl = NormalizeUri(brd.SchemaUrl);
        }
        return boards.ToList();
    }
    private static IEnumerable<Board> FilterAndConvertToBoards(IEnumerable<InternalBmaBoard> boards)
    {
        foreach (var b in boards)
        {
            if (b.Lat.GetValueOrDefault(0) == 0 || b.Lon.GetValueOrDefault(0) == 0)
                continue;

            yield return new Board
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
    }
    private static string NormalizeUri(string uri)
    {
        if (string.IsNullOrEmpty(uri))
            return null;

        //string newlink = uri.Replace("\"", "");

        return Uri.TryCreate(uri, UriKind.Absolute, out Uri u) ? u.ToString() : null;
    }
}
