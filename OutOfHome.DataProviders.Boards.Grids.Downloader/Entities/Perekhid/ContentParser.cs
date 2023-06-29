using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common.Enums;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Parsing;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;
internal class ContentParser : Interfaces.IContentParser<ResponseContent>
{
    public ContentParser()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }
    public async Task<ResponseContent> ParseContent(HttpResponseMessage message)
    {
        var excel = await message.Content.ReadAsStreamAsync();
        return ReadFromFile(excel);
    }
    private ResponseContent ReadFromFile(Stream stream)
    {
        
        List<PerekhidBoard> list;
        DateTime created;
        using (ExcelPackage excel = new ExcelPackage(stream))
        {
            var sheet = excel.Workbook.Worksheets[0];
            int headerRow = ContentParser.FindTableHeaderRow(sheet);
            created = ContentParser.FindCreationTime(sheet);           

            var propertiesFields = ContentParser.ExtractPropertiesFields(sheet, headerRow).ToList();
            var occupationFields = ExtractBoardOccupationFields(propertiesFields).ToList();

            DateOnly visibleStart = occupationFields.Min(a => a.Start);
            DateOnly visibleEnd = occupationFields.Max(a => a.End);
            int periodsCount = occupationFields.Count;

            int rowsNumber = sheet.Dimension.Rows;
            list = new List<PerekhidBoard>(rowsNumber - headerRow + 1);
            for (int i = headerRow + 2; i <= rowsNumber; i++)
            {
                PerekhidBoard board = new PerekhidBoard
                {
                    VisibleStart = visibleStart,
                    VisibleEnd = visibleEnd,
                    OccupationPeriods = new List<OccupationPeriod>(periodsCount)
                };
                foreach (var field in propertiesFields)
                {
                    field.SetPropertyValue(sheet.Cells[i, field.Column], board);
                }

                board.OccupationPeriods = ContentParser.CompressPeriods(board.OccupationPeriods);

                list.Add(board);
            }            
        }
        return new ResponseContent { Boards = list, Time = created };
    }

    private static List<OccupationPeriod> CompressPeriods(List<OccupationPeriod> occupationPeriods)
    {
        OccupationPeriod current = null;
        List < OccupationPeriod > newList = new List<OccupationPeriod>(occupationPeriods.Count);

        for (int i = 0; i < occupationPeriods.Count; i++)
        {
            if (current == null)
            {
                current = occupationPeriods[i];
            }
            else
            { 
                if(current.Condition == occupationPeriods[i].Condition)
                {
                    current.End = occupationPeriods[i].End;
                }
                else
                {
                    newList.Add(current);
                    current = occupationPeriods[i];
                }
            }
        }
        newList.Add(current);
        return newList;
    }

    private static IEnumerable<ExcelFieldBoardOccupation> ExtractBoardOccupationFields(IEnumerable<ExcelFieldBase> fields)
    {
        foreach (var f in fields)
        {
            if (f is ExcelFieldBoardOccupation o)
                yield return o;
        }
    }
    private static int FindTableHeaderRow(ExcelWorksheet worksheet)
    {
        const string headerFirstColumn = "Місто";

        for (int i = 1; i <= 10; i++)
        {
            var value = worksheet.Cells[i, 1].GetValue<string>();
            if(headerFirstColumn.Equals(value, StringComparison.OrdinalIgnoreCase))
                return i;
        }
        throw new Exception($"Заголовок таблицы не найден по ячейке {headerFirstColumn} первого столбца");
    }
    private static DateTime FindCreationTime(ExcelWorksheet worksheet)
    {
        const string checkString = "сетка занятости на ";

        for (int i = 1; i <= 10; i++)
        {
            var value = worksheet.Cells[i, 1].GetValue<string>();
            if (!string.IsNullOrEmpty(value) && value.Contains(checkString, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    return DateTime.ParseExact(value.Replace(checkString, null), "dd.MM.yyyy HH:mm", null);
                }
                catch 
                {
                    return DateTime.Now;
                }                 
            }
        }
        return DateTime.Now;
    }
    private static IEnumerable<ExcelFieldBase> ExtractPropertiesFields(ExcelWorksheet worksheet, int row)
    {
        int numCol = worksheet.Dimension.Columns;

        for (int i = 1; i <= numCol; i++)
        {
            string headerCellValue = worksheet.Cells[row, i].GetValue<string>();
            if(_headersToProperty.TryGetValue(headerCellValue, out BoardProperty property))
            {
                yield return new ExcelFieldBoardProperty(i, property);
            }
            else if(_monthNumbers.TryGetValue(headerCellValue, out int monthNum))
            {
                yield return new ExcelFieldBoardOccupation(i, new DateOnly(DateTime.Now.Year, monthNum, 1));
            }
        }
    }

    private static readonly Dictionary<string, BoardProperty> _headersToProperty = new Dictionary<string, BoardProperty>(StringComparer.OrdinalIgnoreCase)
    {
        { "Місто", BoardProperty.City },
        { "Код", BoardProperty.SupplierCode },
        { "Адреса", BoardProperty.Address },
        { "Конструкція", BoardProperty.Construction },
        { "Формат", BoardProperty.Size },
        { "Сторона", BoardProperty.Side },
        { "Світло", BoardProperty.Light },
        { "№ DOORS", BoardProperty.DoorsId },
        { "OTSnet", BoardProperty.OTS },
        { "GRPnet", BoardProperty.GRP },
        { "прайс, грн без ПДВ", BoardProperty.Price },
        { "Карта", BoardProperty.URL_Map },
    };
    private static readonly Dictionary<string, int> _monthNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        { "янв", 1 },
        { "фев", 2 },
        { "мар", 3 },
        { "апр", 4 },
        { "май", 5 },
        { "июн", 6 },
        { "июл", 7 },
        { "авг", 8 },
        { "сен", 9 },
        { "окт", 10 },
        { "ноя", 10 },
        { "дек", 12 }
    };
}
