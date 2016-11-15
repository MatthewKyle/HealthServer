namespace HealthServer.Models
{
    using System.Threading.Tasks;

    public interface IHealthContext
    {
        HealthResponse Response { get; }
        void AddCheckState(HealthCheckResult result);
        Task<string> CreateResponseAsync();
    }
}