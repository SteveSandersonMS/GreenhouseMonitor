using System.Text.Json;
using System.Text.Json.Serialization;

namespace GreenhouseMonitor.Data;

public class JavaScriptDateConverter : JsonConverter<DateTime>
{
    static readonly DateTime JSEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public override DateTime Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        => JSEpoch.AddMilliseconds(reader.GetInt64());

    public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
	    => writer.WriteNumberValue((long)date.Subtract(JSEpoch).TotalMilliseconds);
}
