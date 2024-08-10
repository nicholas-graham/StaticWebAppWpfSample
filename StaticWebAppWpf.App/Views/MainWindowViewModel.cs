using CommunityToolkit.Mvvm.ComponentModel;

namespace StaticWebAppWpf.App.Views
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty] private string _staticWebSource;
        public MainWindowViewModel()
        {
            StaticWebSource = GetSource();
        }

        public static string GetSource()
        {
            return $"http://localhost:{Program.StaticWebPort}";
        }
    }
}
