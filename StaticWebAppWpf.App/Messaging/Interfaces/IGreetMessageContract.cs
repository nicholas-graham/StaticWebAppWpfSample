using StaticWebAppWpf.App.Messaging.Models;

namespace StaticWebAppWpf.App.Messaging.Interfaces
{
    public interface IGreetMessageContract
    {
        bool ShowMessage { get; set; }
        string SayHello(string name);
        Task<string> SayHelloAfterWait(string name);
        GreetMessageObject SayHelloWithObject(string greetObjectJson);
        void ThrowException();
    }
}