using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Health.Status.Models
{
    public interface IHealthStatusHandler
    {
        string Route { get; }
        Task Execute(HttpContext context);
    }
}