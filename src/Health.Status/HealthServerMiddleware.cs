using System;
using System.Threading.Tasks;
using Health.Status.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Health.Status
{
    public class HealthServerMiddleware
    {
        private readonly ILogger<HealthServerMiddleware> _logger;
        private readonly RequestDelegate next;

        public HealthServerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _logger = loggerFactory.CreateLogger<HealthServerMiddleware>();
        }

        public async Task Invoke(HttpContext context, IHealthStatusHandler handler)
        {
            try
            {
                if (context.Request.Path.StartsWithSegments(new PathString(handler.Route)))
                {
                    _logger.LogDebug($"Calling Handler for Path: {handler.Route}");
                    await handler.Execute(context);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
            finally
            {
                await next.Invoke(context);
            }
        }
    }
}