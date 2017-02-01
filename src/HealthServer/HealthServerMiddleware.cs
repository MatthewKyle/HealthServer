namespace HealthServer
{
    using System;
    using System.Threading.Tasks;

    using HealthServer.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class HealthServerMiddleware
    {
        private readonly ILogger<HealthServerMiddleware> _logger;
        private readonly RequestDelegate next;

        public HealthServerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this._logger = loggerFactory.CreateLogger<HealthServerMiddleware>();
        }

        public async Task Invoke(HttpContext context, IHealthStatusHandler handler)
        {
            try
            {
                if (context.Request.Path.StartsWithSegments(new PathString(handler.Route)) && context.Request.Method == "GET")
                {
                    this._logger.LogDebug($"Calling Handler for Path: {handler.Route}");
                    await handler.Execute(context);
                }
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception.Message);
            }
            finally
            {
                if (!context.Response.HasStarted)
                {
                    await this.next.Invoke(context);
                }
            }
        }
    }
}