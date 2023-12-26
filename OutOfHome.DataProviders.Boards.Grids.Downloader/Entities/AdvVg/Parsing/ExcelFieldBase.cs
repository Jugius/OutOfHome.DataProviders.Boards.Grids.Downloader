using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Parsing;
internal abstract class ExcelFieldBase
{
    public int Column { get; }
    public abstract void SetPropertyValue(ExcelRange cell, Board board);
    public ExcelFieldBase(int column)
    {
        Column = column;
    }
}
