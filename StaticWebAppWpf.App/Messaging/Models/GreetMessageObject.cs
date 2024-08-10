using System.Runtime.InteropServices;

namespace StaticWebAppWpf.App.Messaging.Models
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class GreetMessageObject
    {
        public string Name { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
