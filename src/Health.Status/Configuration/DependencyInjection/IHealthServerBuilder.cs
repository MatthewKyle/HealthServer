using Microsoft.Extensions.DependencyInjection;

namespace Health.Status.Configuration.DependencyInjection
{
    public interface IHealthServerBuilder
    {
        IServiceCollection Services { get; }
    }
}