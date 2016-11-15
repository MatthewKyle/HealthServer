using System;
using Health.Status.Configuration.DependencyInjection.BuilderExtensions;
using Health.Status.Configuration.DependencyInjection.Options;
using Health.Status.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Health.Status.Configuration.DependencyInjection
{
    public static class HealthServerExtensions
    {
        public static IHealthServerBuilder AddHealthServerBuilder(this IServiceCollection services)
        {
            return new HealthServerBuilder(services);
        }

        public static IHealthServerBuilder AddHealthServer(this IServiceCollection services)
        {
            var builder = services.AddHealthServerBuilder();

            builder.AddDefaultServices();

            return builder;
        }

        public static IHealthServerBuilder AddHealthServer(this IServiceCollection services,
            Action<HealthServerOptions> setup)
        {
            services.Configure(setup);

            return services.AddHealthServer();
        }
    }
}