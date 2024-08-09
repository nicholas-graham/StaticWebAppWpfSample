using Microsoft.Extensions.Hosting;
using StaticWebAppWpf.App.Extensions;
using StaticWebAppWpf.App.Utilities;
using System.Windows;
using System.Windows.Threading;

namespace StaticWebAppWpf.App
{
    public class Program
    {
        /// <summary>
        /// The main app host for this WPF application
        /// </summary>
        public static IHost AppHost { get; }

        /// <summary>
        /// The web port to serve static files.
        /// </summary>
        public static int StaticWebPort { get; }

        static Program()
        {
            StaticWebPort = PortUtilities.GetAvailablePort();
            // Set up our DI host inside a static constructor so
            // is set up before the main method is called and never null.

            AppHost = Host.CreateDefaultBuilder()
                .ConfigureProcessMonitorServices()
                .Build();
        }

        static async Task Main()
        {
            try
            {
                // store the apphost and start it while we start loading the WPF dlls 
                var webAppTask = AppHost.RunAsync();

                // start the WPF app using a thread from any state,
                // if we don't have the STA thread, we will create one.
                Dispatcher.CurrentDispatcher.Thread.StartWpfApp();

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
            await AppHost.StopAsync();

            App.Current.Dispatcher.Invoke(App.Current.Shutdown);
        }
    }
}
