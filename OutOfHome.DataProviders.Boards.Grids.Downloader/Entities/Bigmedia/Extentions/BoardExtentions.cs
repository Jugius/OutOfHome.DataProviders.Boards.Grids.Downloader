
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Extentions;
internal static class BoardExtentions
{
    public static Board MapToBoard(this InternalBmaBoard board) =>
        new Board
        {
            Address = board.Address,
            Angle = board.Angle,
            Grp = board.Grp,
            IdCatab = board.IdCatab,
            IdCity = board.IdCity,
            IdNetwork = board.IdNetwork,
            IdSize = board.IdSize,
            IdSupplier = board.IdSupplier,
            Lat = board.Lat,
            Lon = board.Lon,
            Light = board.Light,
            Ots = board.Ots,
            Trp = board.Trp,
            PhotoUrl = board.PhotoUrl,
            Price = board.Price,
            SchemaUrl = board.SchemaUrl,
            Sides = board.Sides,
            SupplierSidetype = board.SupplierSidetype,
        };
}
