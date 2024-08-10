using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace StaticWebAppWpf.App.BackgroundServices
{
    /// <summary>
    /// Runs a basic web application to serve static files to the WebView2 application.
    /// </summary>
    public class StaticWebService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var builder = WebApplication.CreateSlimBuilder();
            var app = builder.Build();
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            Debug.WriteLine($"Starting application, listening on port {Program.StaticWebPort}");
            app.Urls.Add($"http://localhost:{Program.StaticWebPort}");

            return app.RunAsync(stoppingToken);
        }
    }
}
