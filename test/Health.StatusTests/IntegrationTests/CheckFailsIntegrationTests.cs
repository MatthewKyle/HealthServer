using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Health.Status.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Health.StatusTests.IntegrationTests
{
    public class CheckFailsIntegrationTests : IClassFixture<TestFixture<CheckFailsStarup>>
    {
        private HttpClient _client;

        public CheckFailsIntegrationTests(TestFixture<CheckFailsStarup> fixture)
        {
            _client = fixture.Client;
        }
        [Fact]
        public async Task CheckHttpMethod_Get_Health_Fails()
        {
            var response = await _client.GetAsync("/health");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var healthResponse = JsonConvert.DeserializeObject<HealthResponse>(body);

            Assert.NotNull(healthResponse);
            Assert.Equal(2, healthResponse.Results.Count);
            Assert.Equal("Lamda", healthResponse.Results[1].Name);

            JToken result;
            ((JObject) healthResponse.Results[1].Response).TryGetValue("result", StringComparison.OrdinalIgnoreCase  , out result);
            Assert.Equal("bad",result.Value<string>());
        }
    }
}
