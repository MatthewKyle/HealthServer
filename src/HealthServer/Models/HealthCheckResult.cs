namespace HealthServer.Models
{
    public class HealthCheckResult
    {
        public HealthCheckResult(string name, bool success, object response = null)
        {
            this.Name = name;
            this.IsSuccess = success;
            this.Response = response;
        }

        public bool IsSuccess { get; set; }

        public string Name { get; set; }

        public object Response { get; set; }
    }
}