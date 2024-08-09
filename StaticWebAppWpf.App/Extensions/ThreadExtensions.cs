using StaticWebAppWpf.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Extensions
{
    public static class ThreadExtensions
    {
        // Ensure the method is not inlined, so you don't
        // need to load any WPF dlls in the Main method
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void StartWpfApp(this Thread startupThread)
        {
            if (startupThread.GetApartmentState() != ApartmentState.STA)
            {
                // Manually invoke an STA thread if we are not in one
                var staThread = new Thread(InitializeAndRunApp);
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
            }
            else
            {
                InitializeAndRunApp();
            }
        }

        private static void InitializeAndRunApp()
        {
            var app = new App();
            app.StartupUri = new Uri($"/Views/{nameof(MainWindow)}.xaml", UriKind.Relative);
            app.InitializeComponent();
            app.Run();
        }
    }
}
