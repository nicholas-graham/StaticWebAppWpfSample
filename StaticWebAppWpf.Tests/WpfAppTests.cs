using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StaticWebAppWpf.App;
using StaticWebAppWpf.App.Messaging.Interfaces;
using StaticWebAppWpf.App.Messaging.Models;
using StaticWebAppWpf.App.Utilities;
using System.Diagnostics;
using System.Text.Json;

namespace StaticWebAppWpf.Tests
{
    public class WpfAppTests : IClassFixture<ProgramFixture>
    {
        private readonly ProgramFixture _fixture;

        public WpfAppTests(ProgramFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task MainMethod_StartsStaticWebApplication_DynamicPort()
        {
            // Arrange
            var args = new string[]
            {
                "-test",
                "-headless"
            };
            await _fixture.Start(args);
            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync($"http://localhost:{Program.StaticWebPort}/healthz");

            // Assert
            Assert.NotEqual(PortUtilities.DevPort, Program.StaticWebPort);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var statusEnum = Enum.Parse<HealthStatus>(responseString);
            Assert.Equal(HealthStatus.Healthy, statusEnum);
        }

        [Fact]
        public async Task MainMethod_StartsStaticWebApplication_DevPort()
        {
            // Arrange
            var args = new string[]
            {
                "-test",
                "-dev",
                "-headless"
            };
            await _fixture.Start(args);
            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync($"http://localhost:{Program.StaticWebPort}/healthz");

            // Assert
            Assert.Equal(PortUtilities.DevPort, Program.StaticWebPort);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var statusEnum = Enum.Parse<HealthStatus>(responseString);
            Assert.Equal(HealthStatus.Healthy, statusEnum);
        }

        [Fact]
        public async Task GreetMessageContract_SayHello_ReturnsExpectedResults()
        {
            // Arrange
            var args = new string[]
            {
                "-test",
                "-headless"
            };
            await _fixture.Start(args);
            var greetService = Program.AppHost!.Services.GetRequiredService<IGreetMessageContract>();
            greetService.ShowMessage = false;
            var name = "XUnitTests";

            // Act
            var response = greetService.SayHello(name);

            // Assert
            Assert.Equal($"Hello from {name}, this message was generated in .Net, with information from JavaScript.", response);
        }
        [Fact]
        public async Task GreetMessageContract_SayHelloWithObject_ReturnsExpectedResults()
        {
            // Arrange
            var args = new string[]
            {
                "-test",
                "-headless"
            };
            await _fixture.Start(args);
            var greetService = Program.AppHost!.Services.GetRequiredService<IGreetMessageContract>();
            greetService.ShowMessage = false;
            var greetObject = new GreetMessageObject
            {
                Name = "WPF",
                Message = "This is a message inside a .NET object!"
            };

            // Act
            var response = greetService.SayHelloWithObject(JsonSerializer.Serialize(greetObject));

            // Assert
            Assert.Equal(greetObject.Name, response.Name);
            Assert.Equal(greetObject.Message, response.Message);
        }

        [Fact]
        public async Task GreetMessageContract_SayHelloAfterWait_ReturnsExpectedMessageAfterWait()
        {
            // Arrange
            var args = new string[]
            {
                "-test",
                "-headless"
            };
            await _fixture.Start(args);
            var greetService = Program.AppHost!.Services.GetRequiredService<IGreetMessageContract>();
            greetService.ShowMessage = false;
            var name = "XUnitTests";

            // Act
            var sw = new Stopwatch();
            sw.Start();
            var response = await greetService.SayHelloAfterWait(name);
            sw.Stop();

            // Assert
            Assert.Equal($"Hello from {name}, this message was generated in .Net, with information from JavaScript.", response);
            Assert.True(sw.ElapsedMilliseconds >= 1000);
        }
        [Fact]
        public async Task GreetMessageContract_ThrowException_ThrowsException()
        {
            // Arrange
            var args = new string[]
            {
                "-test",
                "-headless"
            };
            await _fixture.Start(args);
            var greetService = Program.AppHost!.Services.GetRequiredService<IGreetMessageContract>();
            greetService.ShowMessage = false;

            // Act / Assert
            Assert.Throws<Exception>(greetService.ThrowException);
        }
    }
}