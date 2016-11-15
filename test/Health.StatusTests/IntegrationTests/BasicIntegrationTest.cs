using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Health.Status.HealthChecks;
using Health.Status.Models;
using Newtonsoft.Json;
using Xunit;

namespace Health.StatusTests.IntegrationTests
{
    public class BasicIntegrationTest : IClassFixture<TestFixture<DefaultStartup>>
    {
        private HttpClient _client;

        public BasicIntegrationTest(TestFixture<DefaultStartup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CheckHttpMethod_Get_Health_Success()
        {
            var response = await _client.GetAsync("/health");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var healthResponse = JsonConvert.DeserializeObject<HealthResponse>(body);

            Assert.NotNull(healthResponse);
            Assert.Equal(typeof(DefaultHealthCheck).Name, healthResponse.Results[0].Name);
        }


    }
}
