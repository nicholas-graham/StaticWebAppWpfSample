using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StaticWebAppWpf.App.Messaging.Models
{
    public class GreetMessageContract
    {
        public string SayHello(string name)
        {
            var msg = $"Hello from {name}, this message was generated in .Net, with information from JavaScript.";
            MessageBox.Show(msg, "WPF Message from JavaScript", MessageBoxButton.OK, MessageBoxImage.Information);
            return msg;
        }

        public string SayGoodbye(string name)
        {
            var msg = $"Goodbye from {name}, this message was generated in .Net, with information from JavaScript.";
            MessageBox.Show(msg, "WPF Message from JavaScript", MessageBoxButton.OK, MessageBoxImage.Information);
            return msg;
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
