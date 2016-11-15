using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Health.Status.Models
{
    public class HealthContext : IHealthContext
    {
        public HealthContext()
        {
            Response = new HealthResponse();
        }

        public HealthResponse Response { get; private set; }

        public void AddCheckState(HealthCheckResult result)
        {
            if (!result.IsSuccess && !Response.IsFailed)
            {
                Response.IsFailed = true;
            }

            Response.Results.Add(result);
        }

        public async Task<string> CreateResponseAsync()
        {
            return await Task<string>.Factory.StartNew(() => JsonConvert.SerializeObject(Response));
        }
    }
}
