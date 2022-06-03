using GreenhouseMonitor.Data;
using static SensorDataBatch.Types;

namespace SensorSimulator;

static class DataGenerator
{
    public static SensorDataBatch NextDataBatch(Building greenhouse)
    {
        var batch = new SensorDataBatch();
        var latestTimestamp = greenhouse.Sensors.Values.Select(s => s.Readings.Last()).Max(r => r.Timestamp);
        var timestamp = latestTimestamp.AddMinutes(5);

        foreach (var sensor in greenhouse.Sensors.Values)
        {
            var newReading = new SensorReading(timestamp, GetSimulatedValue(sensor.SensorId, timestamp));
            sensor.Readings.Enqueue(newReading);

            var sensorData = new SensorData { SensorId = sensor.SensorId };
            sensorData.Entries.Add(new SensorDataEntry { Timestamp = newReading.Timestamp.Ticks, Value = newReading.Value });
            batch.Sensors.Add(sensorData);
        }

        return batch;
    }

    private static double GetSimulatedValue(string sensorId, DateTime timestamp)
    {
        if (sensorId.StartsWith("temp-"))
        {
            return AppData.GetSimulatedTemperatureReading(sensorId.GetHashCode(), timestamp);
        }

        if (sensorId.StartsWith("humidity"))
        {
            return AppData.GetSimulatedHumidityReading(timestamp);
        }

        if (sensorId.StartsWith("moisture-"))
        {
            return AppData.GetSimulatedMoistureReading(sensorId.GetHashCode(), timestamp);
        }

        throw new NotImplementedException(sensorId);
    }
}
