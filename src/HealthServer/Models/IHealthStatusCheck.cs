namespace HealthServer.Models
{
    using System.Threading.Tasks;

    public interface IHealthStatusCheck
    {
        Task Execute(IHealthContext context);

        string Name { get; }
    }
}