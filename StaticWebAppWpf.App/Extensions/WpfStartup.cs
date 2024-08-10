using StaticWebAppWpf.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Extensions
{
    public static class WpfStartup
    {
        /// <summary>
        /// Starts the WPF app on a dedicated STA thread. 
        /// </summary>
        /// <returns>The STA thread of the WPF app.</returns>
        // Ensure the method is not inlined, so you don't
        // need to load any WPF dlls in the Main method
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static Thread StartWpfApp()
        {
            // Manually invoke an STA thread
            var wpfAppThread = new Thread(InitializeAndRunApp);
            wpfAppThread.SetApartmentState(ApartmentState.STA);
            wpfAppThread.Start();
            return wpfAppThread;
        }

        /// <summary>
        /// Initializes the MainWindow of the application and then starts the app. 
        /// </summary>
        private static void InitializeAndRunApp()
        {
            var app = new App();
            app.StartupUri = new Uri($"/Views/{nameof(MainWindow)}.xaml", UriKind.Relative);
            app.InitializeComponent();
            app.Run();
        }
    }
}
