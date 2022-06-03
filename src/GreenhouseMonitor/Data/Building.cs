namespace GreenhouseMonitor.Data;

public class Building
{
    public string Name { get; set; } = "Unnamed";
    public Dictionary<string, Sensor> Sensors { get; set; } = new();

    public void AddSensor(Sensor sensor)
    {
        Sensors[sensor.SensorId] = sensor;
    }
}
