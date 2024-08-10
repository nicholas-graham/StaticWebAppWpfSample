using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App.Extensions;
using StaticWebAppWpf.App.Utilities;
using System.Windows;

namespace StaticWebAppWpf.App
{
    public class Program
    {
        /// <summary>
        /// The main app host for this application
        /// </summary>
        public static IHost? AppHost { get; private set; }

        /// <summary>
        /// The web port to serve static files or the dev server.
        /// </summary>
        public static int? StaticWebPort { get; private set; }

        static async Task Main(string[] args)
        {
            try
            {
                AppHost = Host.CreateDefaultBuilder()
                    .UseEnvironment(EnvironmentParser.GetEnvironment(args))
                    .ConfigureWpfAppServices()
                    .Build();

                StaticWebPort = PortUtilities.GetPort(args);

                // store the apphost long running task and start it while we start loading the WPF dlls 
                var webAppTask = AppHost.RunAsync();

                // start the WPF app on a dedicated STA thread
                WpfApp.Start();

                // await the web task
                await webAppTask;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application has crashed due to {ex.Message}. Shutting down now...");
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

            WpfApp.Shutdown();
        }
    }
}
