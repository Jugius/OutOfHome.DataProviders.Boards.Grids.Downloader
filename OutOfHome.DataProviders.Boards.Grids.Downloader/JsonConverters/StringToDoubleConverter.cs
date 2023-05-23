using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace OutOfHome.DataProviders.Boards.Grids.Downloader.JsonConverters;
internal class StringToDoubleConverter : JsonConverter<double>
{
    private static readonly NumberFormatInfo parsePointFormatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
    public override double Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        double.TryParse(reader.GetString().Trim(), NumberStyles.AllowDecimalPoint, parsePointFormatter, out double value);
        return value;
    }

    public override void Write(
        Utf8JsonWriter writer,
        double value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
