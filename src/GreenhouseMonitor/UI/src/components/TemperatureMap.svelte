<script>
    import { lastReading } from "../data.js";
    import { temperatureCssFilter } from "../colors.js";
    export let greenhouse;
</script>

<div class="temperature-map">
    {#each Object.values(greenhouse.sensors).filter(s => s.sensorId.startsWith('temp-point-')) as sensor}
        <img alt="Temperature map" class="separator" src="temperature-map/greenhouse-separator.png" />
        <img alt class="segment" src="temperature-map/greenhouse-segment.png" style={temperatureCssFilter(greenhouse, sensor.sensorId)} />
        <div class="temp" style={temperatureCssFilter(greenhouse, sensor.sensorId)}>{ Math.round(lastReading(greenhouse, sensor.sensorId)) }</div>
    {/each}

    <img alt class="door" src="temperature-map/greenhouse-door.png" style={temperatureCssFilter(greenhouse, 'temp-point-3')} />
</div>

<style>
    .temperature-map {
        position: relative;
        text-align: center;
        width: 14rem;
    }

    .temperature-map img {
        position: absolute;
        width: 120px;
        --item-start-x: -20px;
        --item-start-y: -12px;
        --item-offset-x: 24.5px;
        --item-offset-y: 11.4px;
        --shadow-filter: drop-shadow(1px 2px 3px rgba(0,0,0,0.2));

        filter: var(--shadow-filter);
        /*filter: hue-rotate(78deg) saturate(1.1) var(--shadow-filter);*/
    }

    .temperature-map img:nth-of-type(1) { transform: translate(var(--item-start-x), var(--item-start-y)) }
    .temperature-map img:nth-of-type(2) { transform: translate(var(--item-start-x), var(--item-start-y)) }
    .temperature-map img:nth-of-type(3) { transform: translate(calc(var(--item-start-x) - 1*var(--item-offset-x)), calc(var(--item-start-y) + 1*var(--item-offset-y))) }
    .temperature-map img:nth-of-type(4) { transform: translate(calc(var(--item-start-x) - 1*var(--item-offset-x)), calc(var(--item-start-y) + 1*var(--item-offset-y))) }
    .temperature-map img:nth-of-type(5) { transform: translate(calc(var(--item-start-x) - 2*var(--item-offset-x)), calc(var(--item-start-y) + 2*var(--item-offset-y))) }
    .temperature-map img:nth-of-type(6) { transform: translate(calc(var(--item-start-x) - 2*var(--item-offset-x)), calc(var(--item-start-y) + 2*var(--item-offset-y))) }
    .temperature-map img:nth-of-type(7) { transform: translate(calc(var(--item-start-x) - 3*var(--item-offset-x)), calc(var(--item-start-y) + 3*var(--item-offset-y))) }
    .temperature-map img:nth-of-type(8) { transform: translate(calc(var(--item-start-x) - 3*var(--item-offset-x)), calc(var(--item-start-y) + 3*var(--item-offset-y))) }
    .temperature-map img:nth-of-type(9) { transform: translate(calc(var(--item-start-x) - 4*var(--item-offset-x)), calc(var(--item-start-y) + 4*var(--item-offset-y))) }

    .temperature-map .temp {
        position: absolute;
        left: calc(50%);
        --item-start-x: -15px;
        --item-start-y: -15px;
        --item-offset-x: 30px;
        --item-offset-y: 14px;
        font-size: 0.7rem;
        color: white;
        background-color: green;
        padding: 0.1rem 0.3rem;
        border-radius: 10rem;
    }

    .temperature-map .temp:nth-of-type(1) { transform: translate(var(--item-start-x), var(--item-start-y)) }
    .temperature-map .temp:nth-of-type(2) { transform: translate(calc(var(--item-start-x) - 1*var(--item-offset-x)), calc(var(--item-start-y) + 1*var(--item-offset-y))) }
    .temperature-map .temp:nth-of-type(3) { transform: translate(calc(var(--item-start-x) - 2*var(--item-offset-x)), calc(var(--item-start-y) + 2*var(--item-offset-y))) }
    .temperature-map .temp:nth-of-type(4) { transform: translate(calc(var(--item-start-x) - 3*var(--item-offset-x)), calc(var(--item-start-y) + 3*var(--item-offset-y))) }

    .temperature-map .temp:after {
        content: '°'
    }

    .temperature-map .door, .temperature-map .separator {
        width: 97px;
        --item-start-x: 3.5px;
        --item-start-y: -12.5px;
    }
</style>