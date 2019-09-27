using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactTest.Outputters;
using WebAPI;
using Xunit;
using Xunit.Abstractions;

namespace PactTest
{
    public class ValuesApiTests: IDisposable, IClassFixture<ValuesApiMock>
    {
        private readonly ITestOutputHelper _output;
        private readonly IWebHost _webHost;
        private string _ServiceUri { get; }

        public ValuesApiTests(ITestOutputHelper output)
        {
            _output = output;
            _ServiceUri = "http://localhost:9000";

            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(_ServiceUri)
                .UseStartup<TestStartup>()
                .Build();
            _webHost.Start();
        }

        [Fact]
        public async Task Pact_Should_Be_Verified()
        {
            var pactConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                    {
                        new XUnitOutputter(_output)
                    },
                Verbose = true
            };
            
            new PactVerifier(pactConfig)
                .ProviderState($"{_ServiceUri}/provider-states")
                .ServiceProvider("otherApi", _ServiceUri)
                .HonoursPactWith("Values")
                .PactUri(@".\pact\values-otherapi.json")
                .Verify();
            
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _webHost.StopAsync().GetAwaiter().GetResult();
                    _webHost.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
