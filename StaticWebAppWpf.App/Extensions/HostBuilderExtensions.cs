using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App.BackgroundServices;
using StaticWebAppWpf.App.Messaging;
using StaticWebAppWpf.App.Messaging.Interfaces;

namespace StaticWebAppWpf.App.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWpfAppServices(this IHostBuilder hostBuilder, string[] args)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                if (!context.HostingEnvironment.IsDevelopment() || args.Contains("-test"))
                {
                    // only add the static web service if we are in production,
                    // in development we'll use the dev server in astro.
                    // test mode always start the service. 
                    services.AddHostedService<StaticWebService>();
                }

                // add our greet message contract.
                services.AddSingleton<IGreetMessageContract, GreetMessageContract>();

                // add services for the WPF portion of the application
                services.AddAppFactory();
            });
        }
    }
}
