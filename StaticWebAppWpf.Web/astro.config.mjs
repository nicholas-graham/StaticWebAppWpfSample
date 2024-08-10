import { defineConfig } from 'astro/config';

// https://astro.build/config
export default defineConfig({
    // Generate a fully static site, and store the build output inside wwwroot for our wpf app.
    output: 'static',
    outDir: '../StaticWebAppWpf.App/wwwroot',
    server: { port: 60000, host: true }
});
