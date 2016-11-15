using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Health.Status.Models;
using Microsoft.AspNetCore.Http;

namespace Health.Status.Handlers
{
    public class DefaultHealthStatusHandler : IHealthStatusHandler
    {
        private readonly IEnumerable<IHealthStatusCheck> _checks;

        public DefaultHealthStatusHandler(IEnumerable<IHealthStatusCheck> checks)
        {
            _checks = checks;
            Route = "/health";
        }

        public string Route { get; }

        public async Task Execute(HttpContext context)
        {
            var healthContext = new HealthContext();
            try
            {
                foreach (var healthStatusCheck in _checks)
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