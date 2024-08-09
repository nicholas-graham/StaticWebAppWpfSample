using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App.BackgroundServices.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureProcessMonitorServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                // Add services here
                services.AddHostedService<StaticWebService>();
            });
        }
    }
}
