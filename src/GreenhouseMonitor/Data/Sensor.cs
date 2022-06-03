using static SensorDataBatch.Types;

namespace GreenhouseMonitor.Data;

public class Sensor
{
    const int MaxEntries = 72;

    public string SensorId { get; }
    public string? DisplayCategory { get; }
    public string? DisplayName { get; }
    public Queue<SensorReading> Readings { get; set; } = new();

    public Sensor(string sensorId, string? displayCategory = null, string? displayName = null)
    {
        SensorId = sensorId;
        DisplayCategory = displayCategory;
        DisplayName = displayName;
    }

    internal void AddEntries(IEnumerable<SensorDataEntry> entries)
    {
        // Remove entries that are too old
        while (Readings.Count > MaxEntries)
            Readings.Dequeue();

        // Add new entries
        foreach (var entry in entries)
            Readings.Enqueue(new SensorReading(entry));
    }
}
