﻿using StaticWebAppWpf.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Extensions
{
    /// <summary>
    /// Wrapper for functions related to starting/stopping the WPF app.
    /// </summary>
    public static class WpfApp
    {
        public static Thread? WpfAppThread { get; private set; }
        /// <summary>
        /// Starts the WPF app on a dedicated STA thread. 
        /// </summary>
        // Ensure the method is not inlined, so you don't
        // need to load any WPF dlls in the Main method
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Start()
        {
            // Manually invoke an STA thread
            var wpfAppThread = new Thread(InitializeAndRunApp);
            wpfAppThread.SetApartmentState(ApartmentState.STA);
            wpfAppThread.Start();
            WpfAppThread = wpfAppThread;
        }

        /// <summary>
        /// Shuts down the WPF app and releases it's thread.
        /// </summary>
        public static void Shutdown()
        {
            App.Current.Dispatcher.Invoke(App.Current.Shutdown);
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