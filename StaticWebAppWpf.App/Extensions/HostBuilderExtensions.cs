using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App.BackgroundServices;
using StaticWebAppWpf.App.Messaging;

namespace StaticWebAppWpf.App.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWpfAppServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                if (!context.HostingEnvironment.IsDevelopment())
                {
                    // only add the static web service if we are in production,
                    // in development we'll use the dev server in astro.
                    services.AddHostedService<StaticWebService>();
                }

                // add our greet message contract.
                services.AddSingleton<IGreetMessageContract, GreetMessageContract>();
                services.AddAppFactory();
            });
        }
    }
}
