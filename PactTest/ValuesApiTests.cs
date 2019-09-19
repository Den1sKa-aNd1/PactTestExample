using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactTest.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace PactTest
{
    public class ValuesApiTests: IDisposable
    {
        private readonly string _serviceUri;
        private readonly IWebHost _webHost;
        private readonly ITestOutputHelper _output;
        public ValuesApiTests(ITestOutputHelper output)
        {
            _serviceUri = "https://localhost:5001";
            _output = output;
        }

        [Fact]
        public void Pact_Should_Be_Verified()
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
            .ServiceProvider("otherApi", _serviceUri)
            .HonoursPactWith("Values")
            .PactUri(@"..\..\..\..\pact\values-otherapi.json")
            .Verify();

        }

        private bool _disposed;

        void IDisposable.Dispose()
        {
            // _pactBuilder.Build();
        }
    }
}
