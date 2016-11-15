namespace HealthServer.Models
{
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class HealthContext : IHealthContext
    {
        public HealthContext()
        {
            this.Response = new HealthResponse();
        }

        public HealthResponse Response { get; private set; }

        public void AddCheckState(HealthCheckResult result)
        {
            if (!result.IsSuccess && !this.Response.IsFailed)
            {
                this.Response.IsFailed = true;
            }

            this.Response.Results.Add(result);
        }

        public async Task<string> CreateResponseAsync()
        {
            return await Task<string>.Factory.StartNew(() => JsonConvert.SerializeObject(this.Response));
        }
    }
}
