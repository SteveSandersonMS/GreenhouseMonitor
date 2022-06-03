using GreenhouseMonitor.Data;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;

namespace GreenhouseMonitor;

class SensorDataReceiverService : SensorDataReceiver.SensorDataReceiverBase
{
    private readonly Building Greenhouse;
    private readonly IHubContext<SensorDataHub> SensorWebsocketContext;

    public SensorDataReceiverService(AppData appData, IHubContext<SensorDataHub> sensorWebsocketContext)
    {
        Greenhouse = appData.Greenhouse;
        SensorWebsocketContext = sensorWebsocketContext;
    }

    public override async Task<StoreDataBatchReply> StoreDataBatch(SensorDataBatch request, ServerCallContext context)
    {
        // Merge the new data into our existing store
        foreach (var incomingSensor in request.Sensors)
            if (Greenhouse.Sensors.TryGetValue(incomingSensor.SensorId, out var existingSensor))
                existingSensor.AddEntries(incomingSensor.Entries);

        // Notify connected clients about new data via websocket
        await SensorWebsocketContext.Clients.All.SendAsync("addData", request);

        return new StoreDataBatchReply { Ok = true };
    }
}
