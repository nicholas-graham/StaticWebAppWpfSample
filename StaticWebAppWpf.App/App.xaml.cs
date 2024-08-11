using StaticWebAppWpf.App.Views;
using System.Windows;

namespace StaticWebAppWpf.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly MainWindow _mainWindow;

        public App(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            _mainWindow.Show();
        }
    }
}
