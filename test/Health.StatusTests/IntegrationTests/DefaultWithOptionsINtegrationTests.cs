namespace HealthServerTests.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Health.StatusTests.IntegrationTests;

    using HealthServer.HealthChecks;
    using HealthServer.Models;

    using HealthServerTests.IntegrationTests.TestStartups;

    using Newtonsoft.Json;

    using Xunit;

    public class DefaultWithOptionsINtegrationTests : IClassFixture<TestFixture<DefaultWithOptionsStartup>>
    {
        private readonly HttpClient _client;

        public DefaultWithOptionsINtegrationTests(TestFixture<DefaultWithOptionsStartup> fixture)
        {
            this._client = fixture.Client;
        }

        [Fact]
        public async Task CheckHttpMethod_Get_Health_Success()
        {
            var response = await this._client.GetAsync("/healthz");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var healthResponse = JsonConvert.DeserializeObject<HealthResponse>(body);

            Assert.NotNull(healthResponse);
            Assert.Equal(typeof(DefaultHealthCheck).Name, healthResponse.Results[0].Name);
        }
    }
}