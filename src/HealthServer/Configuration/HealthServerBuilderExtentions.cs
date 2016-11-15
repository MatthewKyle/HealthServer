namespace HealthServer.Configuration
{
    using System;
    using System.Collections.Generic;

    using HealthServer.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;

    public static class HealthServerBuilderExtentions
    {
        public static IApplicationBuilder UseHealthServer(this IApplicationBuilder builder)
        {
            builder.Validate();

            return builder.UseMiddleware<HealthServerMiddleware>();
        }

        internal static object CheckService(this IApplicationBuilder builder, Type service, ILogger logger)
        {
            var serviceInstance = builder.ApplicationServices.GetService(service);

            if (serviceInstance == null)
            {
                var message = $"A Required Service is missing : {service.Name}";

                logger.LogCritical(message);

                throw new InvalidOperationException(message);
            }

            return serviceInstance;
        }

        internal static void Validate(this IApplicationBuilder builder)
        {
            var loggerFactory = builder.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            var logger = loggerFactory.CreateLogger<HealthServerMiddleware>();

            builder.CheckService(typeof(IHealthStatusHandler), logger);
            builder.CheckService(typeof(IEnumerable<IHealthStatusCheck>), logger);
        }
    }
}