using OfficeOpenXml;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Parsing;
internal class ExcelFieldBoardOccupation : ExcelFieldBase
{
    public DateOnly Start { get; }
    public DateOnly End { get; }

    public ExcelFieldBoardOccupation(int column, DateOnly start) : base(column)
    {
        Start = start;
        End = new DateOnly(start.Year, start.Month, 1).AddMonths(1).AddDays(-1);
    }

    public override void SetPropertyValue(ExcelRange source, PerekhidBoard destination)
    {       
        int condition = GetCondition(source.GetValue<string>());
        destination.OccupationPeriods.Add(new OccupationPeriod { Start = this.Start, End = this.End, Condition = condition });            
    }

    private static int GetCondition(string value)
    { 
        if (string.IsNullOrEmpty(value))
            return 1;

        if(_conditions.TryGetValue(value, out int condition))
            return condition;

        return 5;
    }

    private static readonly Dictionary<string, int> _conditions =
        new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "занят", 3 },
            { "резерв", 2 }
        };


        //Free = 1,
        //Reserved = 2,
        //Booked = 3,
        //Unavailable = 4,
        //Unrecognized = 5,
}
