
using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Parsing;
internal abstract class ExcelFieldBase
{
    public  int Column { get; }
    public abstract void SetPropertyValue(ExcelRange cell, PerekhidBoard board);
    public ExcelFieldBase(int column)
    {
        Column = column;
    }   
}
