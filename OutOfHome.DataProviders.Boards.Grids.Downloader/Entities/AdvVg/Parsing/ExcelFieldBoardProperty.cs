using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Parsing.Enums;
using PuppeteerSharp;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Parsing;
internal class ExcelFieldBoardProperty : ExcelFieldBase
{
    private const string BoardIsLightedFactor = "есть";
    private const byte LightIsOn = 1;
    private const byte LightIsOff = 2;
    private static readonly HashSet<BoardProperty> _required = new()
    {
        BoardProperty.City, BoardProperty.Address,
        BoardProperty.Side, BoardProperty.Construction, BoardProperty.Size
    };
    public BoardProperty Property { get; }
    public bool IsRequired => _required.Contains(this.Property);
    public ExcelFieldBoardProperty(int column, BoardProperty property) : base(column)
    {
        Property = property;
    }

    public override void SetPropertyValue(ExcelRange source, Board destination)
    {
        if (source == null)
        {
            if (IsRequired)
                throw new Exception($"Нулевая ячейка для столбца {Property}. Адрес ячейки {source.Address}");
            else
                return;
        }

        string val = source.GetValue<string>();
        if (string.IsNullOrEmpty(val) && IsRequired)
            throw new Exception($"Недопустимо пустое значение ячейки для свойства {Property}. Адрес ячейки {source.Address}");


        switch (Property)
        {
            case BoardProperty.SupplierCode:
                destination.SupplierCode = val;
                destination.UrlPage = GetUriString(source);
                break;

            case BoardProperty.City:
                destination.City = val;
                break;

            case BoardProperty.Address:
                destination.Address = val;
                break;

            case BoardProperty.District:
                destination.District = val;
                break;

            case BoardProperty.Area:
                destination.Area = val;
                break;

            case BoardProperty.Side:
                destination.Side = val;
                break;

            case BoardProperty.Construction:
                destination.Type = val;
                break;

            case BoardProperty.Size:
                destination.Size = val.Trim().ToLower().Replace('х', 'x');
                break;

            case BoardProperty.Light:
                destination.Lighting = BoardIsLightedFactor.Equals(val, StringComparison.OrdinalIgnoreCase) ? LightIsOn : LightIsOff;
                break;

            case BoardProperty.URL_Map:
                destination.UrlMap = GetUriString(source);
                break;

            case BoardProperty.URLPhoto:
                destination.UrlPhoto = GetUriString(source);
                break;

            case BoardProperty.URL_Schema:
                destination.UrlSchema = GetUriString(source);
                break;

            case BoardProperty.DoorsId:
                destination.DoorsDix = source.GetValue<int?>();
                break;

            case BoardProperty.OTS:
                destination.Ots = source.GetValue<int?>();
                break;

            case BoardProperty.GRP:
                destination.Grp = source.GetValue<float?>();
                break;

            case BoardProperty.Price:
                destination.Price = source.GetValue<int>();
                break;


            
            
            
            default:
                break;
        }
    }
    private static Uri GetUri(ExcelRange cell)
    {
        if (cell == null)
            return null;

        if (cell.Hyperlink != null)
            return cell.Hyperlink;


        if (cell.Value != null)
        {
            string url = cell.GetValue<string>().Trim();
            if (!string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out Uri u))
                return u;
        }

        if (cell.Formula != null)
        {
            string formula = cell.Formula;
            if (formula.Contains("HYPERLINK"))
            {
                int Start, End;
                Start = formula.IndexOf('"', 0) + 1;
                End = formula.IndexOf('"', Start);
                formula = formula.Substring(Start, End - Start);
                if (Uri.TryCreate(Uri.UnescapeDataString(formula), UriKind.Absolute, out Uri u))
                    return u;
            }
        }
        return null;
    }
    private static string GetUriString(ExcelRange cell)
    { 
        var uri = GetUri(cell);
        if (uri == null) return null;
        return uri.ToString();
    }
}
