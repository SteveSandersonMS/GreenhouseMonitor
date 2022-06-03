<script>
    import SensorGraph from "./SensorGraph.svelte";
    import { lastReading } from "../data.js";
    export let greenhouse;
    export let sensor;
</script>

<div class="sensor">
    <div class="sensor-name">
        <div class="sensor-name-category">{sensor.displayCategory} ></div>
        <div class="sensor-name-text">{sensor.displayName}</div>
    </div>
    <SensorGraph readings={sensor.readings} />
    <div class="sensor-value">{ Math.round(lastReading(greenhouse, sensor.sensorId)) }%</div>
    <div class="sensor-options"></div>
</div>

<style>
    .sensor {
        display: flex;
        gap: 1rem;
        background-color: white;
        padding: 0.8rem 1.4rem;
        align-items: center;
        border-radius: 0.3rem;
        margin: 1rem 0;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }

    .sensor-name {
        width: 7rem;
    }

    .sensor-name-category {
        font-size: 0.8rem;
        font-weight: 300;
    }

    .sensor-name-text {
        font-size: 1.2rem;
        margin-top: 0.2rem;
    }

    .sensor-value {
        font-size: 1.4rem;
        width: 3rem;
        text-align: right;
    }

    .sensor-options::after {
        content: '☰';
        color: #888;
    }
</style>
