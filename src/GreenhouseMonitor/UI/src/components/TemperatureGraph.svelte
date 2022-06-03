<script>
    import Line from "svelte-chartjs/src/Line.svelte";
    import { temperatureColor } from "../colors.js";
    export let readings;

    $: data = {
        labels: readings.map(r => r.ts),
        datasets: [{
            data: readings.map(r => r.val),
            fill: true,
            pointRadius: 0,
            segment: {
                borderColor: ctx => `rgb(${temperatureColor(ctx.p1.raw)})`,
                backgroundColor: ctx => `rgba(${temperatureColor(ctx.p1.raw)}, 0.5)`
            }
        }]
    };

    const options = {
        animation: { duration: 0 },
        plugins: { legend: { display: false } },
        maintainAspectRatio: false,
        scales: {
            x: {
                ticks: { 
                    autoSkip: true,
                    maxTicksLimit: 5,
                    maxRotation: 0,
                    minRotation: 0,
                    callback: (value, index, ticks) => { 
                        const d = new Date(data.labels[index]);
                        return `${d.getHours()}:${String(d.getMinutes()).padStart(2, '0')}`;
                    }
                }
            },
            y: {
                ticks: { callback: (value, index, ticks) => `${value}°` },
                grid: { display:false }
            }
        }
    };
</script>

<div class="temperature-graph">
    <Line data={ data } options={ options} />
</div>

<style>
    .temperature-graph {
        flex-grow: 1;
    }

    .temperature-graph :global(canvas) {
        height: 7rem;
    }
</style>
