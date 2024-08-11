using Microsoft.Extensions.DependencyInjection;
using StaticWebAppWpf.App.Factories;
using StaticWebAppWpf.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds the necessary WPF app services to the DI container.
        /// </summary>
        public static void AddAppFactory(this IServiceCollection services)
        {
            // Add our wrapper which does not require the STA thread to construct. 
            services.AddSingleton<WpfApp>();

            // In this sample we have a single view and viewmodel for the app lifetime so we can add them here as singletons. 
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            // Use an abstract factory to construct the App such that we can control the calling thread to be the app STA thread.
            services.AddSingleton<App>();
            services.AddSingleton<Func<App>>(a => () => a.GetRequiredService<App>());
            services.AddSingleton<IAbstractFactory<App>, AbstractFactory<App>>();
        }
    }
}
