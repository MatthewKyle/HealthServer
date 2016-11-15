namespace HealthServer.Configuration.DependencyInjection.BuilderExtensions
{
    using System.Collections.Generic;

    using HealthServer.Configuration.DependencyInjection;
    using HealthServer.Configuration.DependencyInjection.Options;
    using HealthServer.Handlers;
    using HealthServer.HealthChecks;
    using HealthServer.Models;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class Core
    {
        public static IHealthServerBuilder AddDefaultServices(this IHealthServerBuilder builder)
        {
            var defaults = new List<IHealthStatusCheck>() {new DefaultHealthCheck()};
            builder.Services.TryAddSingleton((IEnumerable<IHealthStatusCheck>)defaults);
            builder.Services.TryAddTransient<IHealthStatusHandler, DefaultHealthStatusHandler>();
            builder.Services.AddOptions();

            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<HealthServerHandlerOptions>>().Value);

            return builder;
        }

        public static IHealthServerBuilder AddHealthChecks(this IHealthServerBuilder builder,
            IEnumerable<IHealthStatusCheck> checks)
        {
            builder.Services.AddSingleton(checks);

            return builder;
        }

        public static IHealthServerBuilder AddHealthStatusHandler<T>(this IHealthServerBuilder builder) where T : class, IHealthStatusHandler
        {
            builder.Services.TryAddTransient<T>();

            return builder;
        }
    }
}
