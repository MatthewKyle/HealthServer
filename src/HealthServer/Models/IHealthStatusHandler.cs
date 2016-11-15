namespace HealthServer.Models
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IHealthStatusHandler
    {
        string Route { get; }

        Task Execute(HttpContext context);
    }
}