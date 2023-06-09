using System.Text.Json.Serialization;
using System.Text.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.JsonConverters;

internal class JsonDateOnlyIntConverter : JsonConverter<DateOnly>
{
    private static readonly DateOnly DateUnixEpoch = DateOnly.FromDateTime(DateTime.UnixEpoch);
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateUnixEpoch.AddDays(reader.GetInt32());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((value.ToDateTime(TimeOnly.MinValue) - DateTime.UnixEpoch).Days);
    }
}
