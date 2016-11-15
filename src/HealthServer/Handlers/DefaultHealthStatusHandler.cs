namespace HealthServer.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthServer.Configuration.DependencyInjection.Options;
    using HealthServer.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    public class DefaultHealthStatusHandler : IHealthStatusHandler
    {
        private readonly IEnumerable<IHealthStatusCheck> _checks;

        private readonly IOptions<HealthServerHandlerOptions> options;

        public DefaultHealthStatusHandler(IEnumerable<IHealthStatusCheck> checks, IOptions<HealthServerHandlerOptions> options)
        {
            this._checks = checks;
            this.options = options;
            this.Route = options.Value.DefaultHandlerRoute;
        }

        public string Route { get; private set; }

        public async Task Execute(HttpContext context)
        {
            var healthContext = new HealthContext();
            try
            {
                foreach (var healthStatusCheck in this._checks)
                {
                    await healthStatusCheck.Execute(healthContext);
                }
            }
            catch (Exception exception)
            {
                healthContext.AddCheckState(new HealthCheckResult("Exception", false, exception));
            }
            finally
            {
                if (healthContext.Response.IsFailed)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(await healthContext.CreateResponseAsync());
                }
                else
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(await healthContext.CreateResponseAsync());
                }
            }
        }
    }
}