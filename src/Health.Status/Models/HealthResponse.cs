using System.Collections.Generic;

namespace Health.Status.Models
{
    public class HealthResponse
    {
        public HealthResponse()
        {
            Results = new List<HealthCheckResult>();
        }

        public bool IsFailed { get; set; }

        public List<HealthCheckResult> Results { get; set; }
    }
}