using StaticWebAppWpf.App.Messaging.Models;

namespace StaticWebAppWpf.App.Messaging
{
    public interface IGreetMessageContract
    {
        string SayHello(string name);
        Task<string> SayHelloAfterWait(string name);
        GreetMessageObject SayHelloWithObject(string greetObjectJson);
        void ThrowException();
    }
}