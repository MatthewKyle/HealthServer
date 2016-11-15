using System.Threading.Tasks;

namespace Health.Status.Models
{
    public interface IHealthContext
    {
        HealthResponse Response { get; }
        void AddCheckState(HealthCheckResult result);
        Task<string> CreateResponseAsync();
    }
}