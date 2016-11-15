using Microsoft.Extensions.DependencyInjection;

namespace Health.Status.Configuration.DependencyInjection
{
    public class HealthServerBuilder : IHealthServerBuilder
    {
        public HealthServerBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}