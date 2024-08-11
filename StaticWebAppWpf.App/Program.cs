using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App.Extensions;
using StaticWebAppWpf.App.Utilities;
using System.Windows;

namespace StaticWebAppWpf.App
{
    public class Program
    {
        /// <summary>
        /// The WPF portion of this application.
        /// </summary>
        public static WpfApp? WpfApp { get; private set; }

        /// <summary>
        /// The main DI host for this application
        /// </summary>
        public static IHost? AppHost { get; private set; }

        /// <summary>
        /// The web port to serve static files or the dev server.
        /// </summary>
        public static int? StaticWebPort { get; private set; }

        public static async Task Main(string[] args)
        {
            try
            {
                AppHost = Host.CreateDefaultBuilder()
                    .UseEnvironment(EnvironmentParser.GetEnvironment(args))
                    .ConfigureWpfAppServices(args)
                    .Build();

                // Resolve the wpf app wrapper only, do not load any WPF dlls until
                // we explicitly create the app later.
                WpfApp = AppHost.Services.GetRequiredService<WpfApp>();

                StaticWebPort = PortUtilities.GetPort(args);

                // start the background services while we start loading the WPF dlls 
                var webAppTask = AppHost.RunAsync();

                // optionally skip starting the WPF app.
                if (!args.Contains("-headless"))
                    WpfApp.Start();

                // await the web task
                await webAppTask;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application has crashed due to: {ex.Message}. Shutting down now...");
                await Shutdown();
            }
        }

        /// <summary>
        /// Shuts down the web application serving the static files and then the WPF Application.
        /// </summary>
        public static async Task Shutdown()
        {
            var appShutdownTask = AppHost?.StopAsync();

            if (appShutdownTask != null )
                await appShutdownTask;

            // shut down the Wpf app if it is running.
            if (WpfApp?.WpfAppThread != null &&
                WpfApp.WpfAppThread.IsAlive)
                WpfApp.Shutdown();
        }
    }
}
