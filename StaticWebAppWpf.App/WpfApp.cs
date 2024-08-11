using StaticWebAppWpf.App.Factories;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StaticWebAppWpf.App
{
    /// <summary>
    /// Wrapper for the WPF application
    /// </summary>
    public class WpfApp
    {
        private readonly IAbstractFactory<App> _appFactory;

        public App? App { get; private set; }
        public Thread? WpfAppThread { get; private set; }

        public WpfApp(IAbstractFactory<App> appFactory)
        {
            _appFactory = appFactory;
        }

        /// <summary>
        /// Resolves the UI services and starts the WPF app on a dedicated STA thread. 
        /// </summary>
        // Ensure the method is not inlined, so you don't
        // need to load any WPF dlls in the Main method
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public void Start()
        {
            var wpfAppThread = new Thread(InitializeAndRunApp);
            wpfAppThread.SetApartmentState(ApartmentState.STA);
            wpfAppThread.Start();
            WpfAppThread = wpfAppThread;
        }

        /// <summary>
        /// Shuts down the WPF app and releases it's thread.
        /// </summary>
        public void Shutdown()
        {
            Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
        }

        /// <summary>
        /// Initializes the application and then starts the app. 
        /// </summary>
        private void InitializeAndRunApp()
        {
            // Call the factory inside our STA thread so all UI
            // services get resolved on this thread.
            App = _appFactory.Create();
            App.Run();
        }
    }
}
