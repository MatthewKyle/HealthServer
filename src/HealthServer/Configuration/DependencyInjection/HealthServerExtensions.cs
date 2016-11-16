namespace HealthServer.Configuration.DependencyInjection
{
    using System;

    using HealthServer.Configuration.DependencyInjection.BuilderExtensions;
    using HealthServer.Configuration.DependencyInjection.Options;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class HealthServerExtensions
    {
        public static IHealthServerBuilder AddHealthServer(this IServiceCollection services)
        {
            var builder = services.AddHealthServerBuilder();

            builder.AddDefaultServices();

            return builder;
        }

        public static IHealthServerBuilder AddHealthServer(
            this IServiceCollection services,
            Action<HealthServerHandlerOptions> setup)
        {
            services.Configure(setup);

            return services.AddHealthServer();
        }

        public static IHealthServerBuilder AddHealthServer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<HealthServerHandlerOptions>(configuration.GetSection("HealthServerHandlerOptions"));

            return services.AddHealthServer();
        }

        public static IHealthServerBuilder AddHealthServerBuilder(this IServiceCollection services)
        {
            return new HealthServerBuilder(services);
        }
    }
}