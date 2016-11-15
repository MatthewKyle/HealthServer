namespace HealthServer.Configuration.DependencyInjection
{
    using System;

    using HealthServer.Configuration.DependencyInjection.BuilderExtensions;
    using HealthServer.Configuration.DependencyInjection.Options;

    using Microsoft.Extensions.DependencyInjection;

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
            Action<HealthServerHandlerOptions> setup)
        {
            services.Configure(setup);

            return services.AddHealthServer();
        }
    }
}