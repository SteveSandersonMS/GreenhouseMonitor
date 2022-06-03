import { lastReading } from './data.js';

const colorMap = [
    '0, 128, 255',  // very low
    '0, 192, 192',  // low
    '0, 200, 100',  // ok-low
    '0, 200, 0',   // perfect
    '255, 190, 0', // ok-high
    '255, 90, 0', // high
    '255, 0, 0',  // very high
];

function temperatureRating(temp) {
    const min = 8;
    const max = 35;
    return Math.max(0, Math.min(1, (temp - min) / (max - min)));
}

export function temperatureColor(temp) {
    const prop = temperatureRating(temp);
    const index = Math.round(prop*(colorMap.length - 1));
    return colorMap[index];
}

export function temperatureHueRotation(temp) {
    const prop = temperatureRating(temp);
    return 80 - 160*prop;
}

export function temperatureCssFilter(greenhouse, sensorId) {
    const temperature = lastReading(greenhouse, sensorId);
    const hueRotation = temperatureHueRotation(temperature);
    return `filter: hue-rotate(${hueRotation}deg)`;
}

export function moistureColor(percent) {
    const min = 5;
    const max = 55;
    const prop = Math.max(0, Math.min(1, (percent - min) / (max - min)));
    const index = Math.round(prop*(colorMap.length - 1));
    return colorMap[index];
}
