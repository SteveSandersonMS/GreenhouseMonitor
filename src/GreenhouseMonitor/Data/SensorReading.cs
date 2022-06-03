using System.Text.Json.Serialization;

namespace GreenhouseMonitor.Data;

public struct SensorReading
{
    [JsonPropertyName("ts")]
    [JsonConverter(typeof(JavaScriptDateConverter))]
    public DateTime Timestamp { get; set; }

    // TODO: Consider sending this as lower-resolution data. No need for 8 decimal places here.
    [JsonPropertyName("val")]
    public double Value { get; set; }

    public SensorReading(DateTime timestamp, double value)
    {
        Timestamp = timestamp;
        Value = value;
    }

    internal SensorReading(SensorDataBatch.Types.SensorDataEntry entry) : this()
    {
        Timestamp = new DateTime(entry.Timestamp);
        Value = entry.Value;
    }
}
