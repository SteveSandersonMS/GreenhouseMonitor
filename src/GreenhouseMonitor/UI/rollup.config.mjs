import svelte from 'rollup-plugin-svelte';
import resolve from '@rollup/plugin-node-resolve';
import css from 'rollup-plugin-css-only';

export default {
  input: 'src/main.js',
  output: {
    file: '../wwwroot/dist/ui.js',
    format: 'iife'
  },
  plugins: [
    svelte(),
    css({ output: 'ui.css' }),
    resolve({ browser: true }),
  ]
};
