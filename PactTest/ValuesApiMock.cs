using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace PactTest
{
    public class ValuesApiMock: IDisposable
    {
        private readonly IPactBuilder _pactBuilder;
        public IMockProviderService MockProviderService { get; }
        public readonly int ServicePort = 9222;

        public string ServiceUri => $"http://localhost:{ServicePort}";

        public ValuesApiMock()
        {
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"..\..\..\pact",
                LogDir = @".\pact_logs"
            };
            _pactBuilder = new PactBuilder(pactConfig)
                .ServiceConsumer("Values")
                .HasPactWith("otherApi");

            MockProviderService = _pactBuilder.MockService(ServicePort, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        void IDisposable.Dispose()
        {
            _pactBuilder.Build();
        }
    }
}
