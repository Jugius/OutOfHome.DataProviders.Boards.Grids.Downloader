using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Parsing;
internal class ExcelFieldBoardOccupation : ExcelFieldBase
{
    public DateOnly Start { get; }
    public DateOnly End { get; }

    public ExcelFieldBoardOccupation(int column, DateOnly start) : base(column)
    {
        Start = start;
        End = new DateOnly(start.Year, start.Month, 1).AddMonths(1).AddDays(-1);
    }

    public override void SetPropertyValue(ExcelRange source, Board destination)
    {
        int condition = GetCondition(source.GetValue<string>());
        destination.OccupationPeriods.Add(new OccupationPeriod { Start = Start, End = End, Condition = condition });
    }

    private static int GetCondition(string value)
    {
        if (string.IsNullOrEmpty(value))
            return OccupationConditions.Free;

        if (_conditions.TryGetValue(value, out int condition))
            return condition;

        return OccupationConditions.Unrecognized;
    }

    private static readonly Dictionary<string, int> _conditions =
        new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "занят", OccupationConditions.Booked },
            { "резерв", OccupationConditions.Reserved }
        };
}
