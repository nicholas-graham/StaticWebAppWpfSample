using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Threading;

namespace StaticWebAppWpf.App.BackgroundServices
{
    /// <summary>
    /// Runs a basic web application to serve static files to the WebView2 application.
    /// </summary>
    public class StaticWebService : BackgroundService
    {
        private WebApplication? _app;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var builder = WebApplication.CreateSlimBuilder();
            builder.Services.AddHealthChecks();

            _app = builder.Build();
            _app.UseDeveloperExceptionPage();

            _app.UseDefaultFiles();
            _app.UseStaticFiles();

            Debug.WriteLine($"Starting application, listening on port {Program.StaticWebPort}");
            _app.Urls.Add($"http://localhost:{Program.StaticWebPort}");

            _app.MapHealthChecks("/healthz");

            await _app!.RunAsync(stoppingToken);
        }
    }
}
