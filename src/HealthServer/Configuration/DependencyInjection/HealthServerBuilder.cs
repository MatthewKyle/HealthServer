namespace HealthServer.Configuration.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public class HealthServerBuilder : IHealthServerBuilder
    {
        public HealthServerBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }
    }
}