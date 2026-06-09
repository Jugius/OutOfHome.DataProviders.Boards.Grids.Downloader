using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Octagon;

public class OccupancyStatusTests(ITestOutputHelper output) : TestBase(output)
{
    [Fact]
    public void DeserializeInvalidOccupancyStatus()
    {
        var str = """{"Status":5}""";
        var wrapper = System.Text.Json.JsonSerializer.Deserialize<OccupancyStatusWrapper>(str);
        Assert.NotNull(wrapper);
        
        Log($"{wrapper.Status}");
        Assert.Equal(5, (int)wrapper.Status);

        var newWrapper = new OccupancyStatusWrapper(wrapper.Status);
        var sstr = System.Text.Json.JsonSerializer.Serialize(newWrapper);
        Log(sstr);
        Assert.Equal(str, sstr);

        Assert.Throws(typeof(Exception), () => Convert(newWrapper.Status));
    }


    private sealed record OccupancyStatusWrapper(OccupancyStatus Status);
    private enum StatusExternal
    {
        Unknown = 0,
        Booked = 1,
        Reserved = 2,
        Free = 3
    }
    private static StatusExternal Convert(OccupancyStatus status) => status switch
    {
        OccupancyStatus.Unknown => StatusExternal.Unknown,
        OccupancyStatus.Booked => StatusExternal.Booked,
        OccupancyStatus.Reserved => StatusExternal.Reserved,
        OccupancyStatus.Free => StatusExternal.Free,
        _ => throw new Exception($"{status}")
    };
}
