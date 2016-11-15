namespace Health.Status.Models
{
    public class HealthCheckResult
    {
        public HealthCheckResult(string name, bool success, object response = null)
        {
            Name = name;
            IsSuccess = success;
            Response = response;
        }

        public bool IsSuccess { get; set; }

        public string Name { get; set; }

        public object Response { get; set; }
    }
}