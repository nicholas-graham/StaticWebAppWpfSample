using StaticWebAppWpf.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StaticWebAppWpf.Tests
{
    public class ProgramFixture : IAsyncDisposable
    {
        public ProgramFixture() { }

        public async Task Start(string[] args)
        {
            var programTask = Program.Main(args);

            // return early if there was an exception or
            // the main program task has returned.
            if (programTask.IsCompleted)
                return;

            using var client = new HttpClient();
            HttpResponseMessage? response = null;

            // wait for the program to finish starting the web application
            while ((response == null || !response.IsSuccessStatusCode))
            {
                if (Program.StaticWebPort != null)
                {
                    try
                    {
                        response = await client.GetAsync($"http://localhost:{Program.StaticWebPort}/healthz");
                    }
                    catch
                    {  
                        // swallow until startup completed.
                    }
                }

                await Task.Delay(200);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await Program.Shutdown();
        }
    }
}
