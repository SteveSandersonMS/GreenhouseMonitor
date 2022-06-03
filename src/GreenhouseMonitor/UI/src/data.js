import * as signalR from '@microsoft/signalr';

let greenhouse;

export async function fetchInitialData() {
    const response = await fetch('/api/greenhouse');
    greenhouse = await response.json();
    updateAverageTemperature();
    return greenhouse;
}

export function readings(greenhouse, sensorId) {
    return greenhouse.sensors[sensorId].readings;
}

export function lastReading(greenhouse, sensorId) {
    return readings(greenhouse, sensorId).slice(-1)[0].val;
}

export function onDataChanged(callback) {
    // We're assuming there will only be one call to this function (ever). If that were not the case,
    // we'd want to have some way of unsubscribing from notifications, and possibly sharing the
    // connection instance across multiple subscriptions.
    const connection = new signalR.HubConnectionBuilder().withUrl("/sensorDataHub").build();
    connection.on('addData', data => {
        // Merge the new data into our client-side store
        // NOTE: There's very likely some way to make chart.js update incrementally instead of completely
        // regenerating the graphs from scratch. However that's not the key point of this demo.
        data.sensors.forEach(updatedSensor => {
            const existingSensor = greenhouse.sensors[updatedSensor.sensorId];
            if (existingSensor) {
                existingSensor.readings.push(...updatedSensor.entries.map(e => ({
                    ts: Math.floor((e.timestamp - 621355968000000000) / 10000), // .NET ticks to JS date value
                    val: e.value
                })));

                // Remove data that's now too old
                existingSensor.readings.splice(0, Math.max(0, existingSensor.readings.length - 72));
            }
        });

        updateAverageTemperature();
        callback(greenhouse);
    });
    
    connection.start();
    window.onbeforeunload = () => { connection.stop(); };
}

function updateAverageTemperature() {
    const series = Object.values(greenhouse.sensors).filter(s => s.sensorId.startsWith('temp-point-')).map(s => s.readings);
    const result = new Map();
    series.forEach(s => {
        s.forEach(r => {
            const ts = 5*60*1000 * Math.round(r.ts / (5*60*1000));
            let existingEntry = result.get(ts);
            if (!existingEntry) {
                result.set(ts, { count: 1, sum: r.val });
            } else {
                existingEntry.count++;
                existingEntry.sum += r.val;
            }
        });
    });

    greenhouse.sensors['temp-avg'] = {
        sensorId: 'temp-avg',
        readings: Array.from(result).map(entry => ({ ts: entry[0], val: entry[1].sum / entry[1].count }))
    };
}
