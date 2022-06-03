<script>
    import HeaderBar from "./HeaderBar.svelte";
    import Sensor from "./Sensor.svelte";
    import TemperatureGraph from "./TemperatureGraph.svelte";
    import TemperatureMap from "./TemperatureMap.svelte";
    import { onMount } from 'svelte';
    import { fetchInitialData, lastReading, readings, onDataChanged } from '../data.js';
    import { temperatureCssFilter } from '../colors.js';

    let greenhouse = {};

    onMount(async () => {
        greenhouse = await fetchInitialData();

        onDataChanged(newGreenhouse => {
            greenhouse = newGreenhouse;
        });
    });
</script>

<HeaderBar greenhouse={ greenhouse } />

<main>
    <div class="content">
        {#if greenhouse.name}
            <div class="greenhouse-overview">
                <TemperatureMap greenhouse={ greenhouse } />
                <div class="temperature-value" style={temperatureCssFilter(greenhouse, 'temp-avg')}>{ Math.round(lastReading(greenhouse, 'temp-avg')) }</div>
                <div class="humidity-value">{ Math.round(lastReading(greenhouse, 'humidity')) }</div>
                <TemperatureGraph readings={ readings(greenhouse, 'temp-avg') } />
            </div>

            <h2>Moisture sensors</h2>
            {#each Object.values(greenhouse?.sensors || {}) as sensor}
                {#if sensor.sensorId.startsWith('moisture-plant-')}
                    <Sensor greenhouse={ greenhouse } sensor={sensor} />
                {/if}
            {/each}
        {/if}
    </div>
</main>

<style>
    h2 {
        margin: 3rem 0 1rem 0;
        font-weight: 300;
    }

    .greenhouse-overview {
        display: flex;
        gap: 1rem;
        align-items: stretch;
    }

    .temperature-value, .humidity-value {
        min-width: 2rem;
        position: relative;
    }

    .temperature-value, .humidity-value {
        width: 6rem;
        height: 6rem;
        flex-grow: 0;
        line-height: 5rem;
        text-align: center;
        font-size: 1.7rem;
        color: white;
    }

    .temperature-value::after {
        content: '°C';
        position: absolute;
        font-size: 0.9rem;
        top: -0.3rem;
    }

    .temperature-value::before, .humidity-value::before {
        position: absolute;
        font-size: 0.8rem;
        left: 0; right: 0;
        bottom: 1rem;
        height: 1rem;
        line-height: 1rem;
        font-weight: 300;
    }

    .temperature-value::before {
        content: 'avg temp';
    }

    .humidity-value::before {
        content: 'humidity';
    }

    .humidity-value::after {
        content: '%';
        position: absolute;
        font-size: 1rem;
        top: -0.3rem;
    }

    .temperature-value {
        background-color: green;
    }

    .humidity-value {
        background-color: #666;
    }
</style>
