namespace HealthServer.Models
{
    using System.Collections.Generic;

    public class HealthResponse
    {
        public HealthResponse()
        {
            this.Results = new List<HealthCheckResult>();
        }

        public bool IsFailed { get; set; }

        public List<HealthCheckResult> Results { get; set; }
    }
}