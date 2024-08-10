using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;

namespace StaticWebAppWpf.App.Messaging.Models
{
    /// <summary>
    /// A simple contract to demonstrate executing different functions in .Net.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class GreetMessageContract
    {
        public string SayHello(string name)
        {
            var msg = $"Hello from {name}, this message was generated in .Net, with information from JavaScript.";
            MessageBox.Show(msg, "WPF Message from JavaScript", MessageBoxButton.OK, MessageBoxImage.Information);
            return msg;
        }
        
        public GreetMessageObject SayHelloWithObject(string greetObjectString)
        {
            // NOTE: it is not possible to pass in an object directly as a parameter,
            // it must be serialized, though we can return an object just fine.
            var greetObject = JsonSerializer.Deserialize<GreetMessageObject>(greetObjectString);
            var msg = $"Hello from {greetObject?.Name}, message is {greetObject?.Message}";
            MessageBox.Show(msg, "WPF Message from JavaScript", MessageBoxButton.OK, MessageBoxImage.Information);

            return new GreetMessageObject
            {
                Name = "WPF",
                Message = "This is a message inside a .NET object!"
            };
        }

        public async Task<string> SayHelloAfterWait(string name)
        {
            await Task.Delay(1000);
            return SayHello(name);
        }

        public void ThrowException()
        {
            throw new Exception("This is an exception thrown in .Net");
        }
    }
}
