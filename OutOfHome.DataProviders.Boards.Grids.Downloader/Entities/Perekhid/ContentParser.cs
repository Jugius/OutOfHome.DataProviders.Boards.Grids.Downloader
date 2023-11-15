using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common.Enums;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Parsing;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;
public class ContentParser : Interfaces.IContentParser<ResponseContent>
{
    public ContentParser()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }
    public async Task<ResponseContent> ParseContent(HttpResponseMessage message)
    {
        using var stream = await message.Content.ReadAsStreamAsync();
        try
        {
            return ReadFromFile(stream);
        }
        catch (Exceptions.DownloaderException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ContentParsingError, "Ошибка чтения контента: " + ex.GetBaseException().Message);
        }
    }
    public ResponseContent ReadFromFile(Stream stream)
    {        
        List<PerekhidBoard> list;
        DateTime created;
        using (ExcelPackage excel = new ExcelPackage(stream))
        {
            var sheet = excel.Workbook.Worksheets[0];
            int headerRow = ContentParser.FindTableHeaderRow(sheet);
            created = ContentParser.FindCreationTime(sheet);           

            var propertiesFields = ExtractPropertiesFields(sheet, headerRow).ToList();
            var occupationFields = ExtractBoardOccupationFields(propertiesFields).ToList();

            //если количество столбцов с занятостью == 0 то выкидываем ошибку
            if (occupationFields.Count == 0)
                throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ContentParsingError, "Не найдены столбцы с занятостью");

            int supplierCodeColumn = propertiesFields.FirstOrDefault(a => a is ExcelFieldBoardProperty p && p.Property == BoardProperty.SupplierCode).Column;


            DateOnly visibleStart = occupationFields.Min(a => a.Start);
            DateOnly visibleEnd = occupationFields.Max(a => a.End);
            int periodsCount = occupationFields.Count;

            int rowsNumber = sheet.Dimension.Rows;
            list = new List<PerekhidBoard>(rowsNumber - headerRow + 1);
            for (int i = headerRow + 2; i <= rowsNumber; i++)
            {
                //Проверяем содержимое ячейки в столбце операторского кода,
                //если код парсится в int, значит место баинговое, игнорируем
                if (int.TryParse(sheet.Cells[i, supplierCodeColumn].GetValue<string>(), out _))
                {
                    continue;
                }                

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
        throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ContentParsingError, $"Заголовок таблицы не найден по ключевому слову '{headerFirstColumn}' для первого столбца");
    }
    private static DateTime FindCreationTime(ExcelWorksheet worksheet)
    {
        const string checkStringRu = "сетка занятости на ";
        const string checkStringUa = "сітка зайнятості на ";

        for (int i = 1; i <= 10; i++)
        {
            var value = worksheet.Cells[i, 1].GetValue<string>();
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains(checkStringRu, StringComparison.OrdinalIgnoreCase))
                {
                    return ExtractCreationDateTime(value, checkStringRu);
                }
                else if (value.Contains(checkStringUa, StringComparison.OrdinalIgnoreCase))
                { 
                    return ExtractCreationDateTime(value,checkStringUa);
                }               
            }
        }
        return DateTime.Now;
    }
    private static DateTime ExtractCreationDateTime(string cellValue, string removingString)
    {
        try
        {
            return DateTime.ParseExact(cellValue.Replace(removingString, null, StringComparison.OrdinalIgnoreCase), "dd.MM.yyyy HH:mm", null);
        }
        catch
        {
            return DateTime.Now;
        }
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
            else if(TryParseStartDay(headerCellValue, out DateOnly date))
            {
                yield return new ExcelFieldBoardOccupation(i, date);
            }
        }
    }

    private static bool TryParseStartDay(string value, out DateOnly result)
    {
        int monthNum;

        // проверяем значение на наличие в словаре, если ДА, значит это значение месяца текущего года, напр. "ГРУ" - декабрь текущего года.
        if (_monthNumbers.TryGetValue(value, out monthNum))
        {
            result = new DateOnly(DateTime.Now.Year, monthNum, 1);
            return true;
        }

        // режем значение по ' ', должно получиться два значения, 1 - название месяца, 2 - год, напр "КВІ 24" - апрель 2024.
        var values = value.Split(' ');

        if (values != null 
            && values.Length == 2
            && _monthNumbers.TryGetValue(values[0], out monthNum) 
            && int.TryParse(values[1], out int yearNum))
        {
            result = new DateOnly(2000 + yearNum,  monthNum, 1);
            return true;
        }

        result = DateOnly.MinValue;
        return false;
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
        { "ноя", 11 },
        { "дек", 12 },


        { "СІЧ", 1 },
        { "ЛЮТ", 2 },
        { "БЕР", 3 },
        { "КВІ", 4 },
        { "ТРА", 5 },
        { "ЧЕР", 6 },
        { "ЛИП", 7 },
        { "СЕР", 8 },
        { "ВЕР", 9 },
        { "ЖОВ", 10 },
        { "ЛИС", 11 },
        { "ГРУ", 12 }
    };
}
