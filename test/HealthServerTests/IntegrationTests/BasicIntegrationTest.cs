using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HealthServer.HealthChecks;
using HealthServer.Models;
using HealthServerTests.IntegrationTests.TestStartups;
using Newtonsoft.Json;
using Xunit;

namespace HealthServerTests.IntegrationTests
{
    public class BasicIntegrationTest : IClassFixture<TestFixture<DefaultStartup>>
    {
        private readonly HttpClient _client;

        public BasicIntegrationTest(TestFixture<DefaultStartup> fixture)
        {
            this._client = fixture.Client;
        }

        [Fact]
        public async Task CheckHttpMethod_Get_Health_Success()
        {
            var response = await this._client.GetAsync("/health");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var healthResponse = JsonConvert.DeserializeObject<HealthResponse>(body);

            Assert.NotNull(healthResponse);
            Assert.Equal(typeof(DefaultHealthCheck).Name, healthResponse.Results[0].Name);
        }

        [Fact]
        public async Task CheckHttpMethod_POST_Health_Fails()
        {
            var response = await this._client.PostAsync("/health", new StringContent(""));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}