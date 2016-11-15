using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health.Status.Handlers;
using Health.Status.HealthChecks;
using Health.Status.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Health.Status.Configuration.DependencyInjection.BuilderExtensions
{
    public static class Core
    {
        public static IHealthServerBuilder AddDefaultServices(this IHealthServerBuilder builder)
        {
            var defaults = new List<IHealthStatusCheck>() {new DefaultHealthCheck()};
            builder.Services.AddSingleton((IEnumerable<IHealthStatusCheck>)defaults);
            builder.Services.TryAddTransient<IHealthStatusHandler, DefaultHealthStatusHandler>();
           
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
