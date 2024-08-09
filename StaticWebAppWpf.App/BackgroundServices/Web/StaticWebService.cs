using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.BackgroundServices.Web
{
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
