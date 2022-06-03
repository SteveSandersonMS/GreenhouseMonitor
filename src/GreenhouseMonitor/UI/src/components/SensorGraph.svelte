<script>
    import Line from "svelte-chartjs/src/Line.svelte";
    import { moistureColor } from "../colors.js";
    export let readings;

    $: data = {
        labels: readings.map(r => r.ts),
        datasets: [{
            data: readings.map(r => r.val),
            fill: true,
            pointRadius: 0,
            segment: {
                borderColor: ctx => `rgb(${moistureColor(ctx.p1.raw)})`,
                backgroundColor: ctx => `rgba(${moistureColor(ctx.p1.raw)}, 0.5)`
            }
        }]
    };

    const options = {
        animation: { duration: 0 },
        plugins: { legend: { display: false } },
        maintainAspectRatio: false,
        scales: {
            x: { display: false },
            y: { display: true }
        }
    };
</script>

<div class="sensor-graph">
    <Line data={data} options={options} />
</div>

<style>
    .sensor-graph {
        flex-grow: 1;
        height: 2.5rem;
    }

    :global(.sensor-graph canvas) {
        margin: -0.6rem 0;
    }
</style>
