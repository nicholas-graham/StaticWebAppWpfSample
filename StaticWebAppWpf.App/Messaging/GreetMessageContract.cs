using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;
using StaticWebAppWpf.App.Messaging.Interfaces;
using StaticWebAppWpf.App.Messaging.Models;

namespace StaticWebAppWpf.App.Messaging
{
    /// <summary>
    /// A simple contract to demonstrate executing different functions in .Net.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class GreetMessageContract : IGreetMessageContract
    {
        public bool ShowMessage { get; set; } = true;
        /// <summary>
        /// Simple sample generates a message with a name from JavaScript, then returns that message back to JS.
        /// </summary>
        public string SayHello(string name)
        {
            var msg = $"Hello from {name}, this message was generated in .Net, with information from JavaScript.";

            if (ShowMessage)
                MessageBox.Show(msg, "WPF Message from JavaScript", MessageBoxButton.OK, MessageBoxImage.Information);

            return msg;
        }

        /// <summary>
        /// Demonstrates how objects can be passed from JS -> .Net -> JS
        /// </summary>
        /// <param name="greetObjectJson">The object coming from JavaScript, at the moment only single parameter functions and primitive types are supported.</param>
        /// <returns>The message object to JavaScript, it may be accessed via an object proxy.</returns>
        public GreetMessageObject SayHelloWithObject(string greetObjectJson)
        {
            // NOTE: it is not possible to pass in an object directly as a parameter,
            // it must be serialized, though we can return an object just fine and it becomes an object proxy in JS.
            var greetObject = JsonSerializer.Deserialize<GreetMessageObject>(greetObjectJson);
            var msg = $"Hello from {greetObject?.Name}, message is {greetObject?.Message}";

            if (ShowMessage)
                MessageBox.Show(msg, "WPF Message from JavaScript", MessageBoxButton.OK, MessageBoxImage.Information);

            return new GreetMessageObject
            {
                Name = "WPF",
                Message = "This is a message inside a .NET object!"
            };
        }

        /// <summary>
        /// Demonstrates that async work can also be completed within a contract.
        /// </summary>
        public async Task<string> SayHelloAfterWait(string name)
        {
            await Task.Delay(1000);
            return SayHello(name);
        }

        /// <summary>
        /// Demonstrates that Exceptions are returned back to the running JS and can be caught/handled there.
        /// </summary>
        public void ThrowException()
        {
            throw new Exception("This is an exception thrown in .Net");
        }
    }
}
