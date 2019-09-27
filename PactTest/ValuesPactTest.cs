using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using WebAPI.Models;
using Xunit;
namespace PactTest
{
    public class ValuesPactTest: IClassFixture<ValuesApiMock>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _serviceUri;
        public ValuesPactTest(ValuesApiMock fixture)
        {
            _mockProviderService = fixture.MockProviderService;
            _mockProviderService.ClearInteractions();
            _serviceUri = fixture.ServiceUri;
        }

        [Fact]
        public async Task Given_Valid_Id_Should_Return_Dto()
        {
            _mockProviderService
                .Given("Some value")
                .UponReceiving("A GET Request")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/api/values"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = Match.Type(new { NumberParam = 888, StringParam = "fake"})
                });
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_serviceUri}/api/values");
            var json = await response.Content.ReadAsStringAsync();
            var someDto = JsonConvert.DeserializeObject<SomeDto>(json);

            Assert.Equal(888, someDto.NumberParam);
            
            _mockProviderService.VerifyInteractions();
        }
    }
}
