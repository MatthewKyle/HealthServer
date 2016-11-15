namespace HealthServer.Configuration.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IHealthServerBuilder
    {
        IServiceCollection Services { get; }
    }
}