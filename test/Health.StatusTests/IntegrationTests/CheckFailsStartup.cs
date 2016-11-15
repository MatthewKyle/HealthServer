using System.Collections.Generic;
using Health.Status.Configuration;
using Health.Status.Configuration.DependencyInjection;
using Health.Status.Configuration.DependencyInjection.BuilderExtensions;
using Health.Status.HealthChecks;
using Health.Status.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Health.StatusTests.IntegrationTests
{
    public class CheckFailsStarup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthServer().AddHealthChecks(new List<IHealthStatusCheck>()
            {
                new DefaultHealthCheck(),
                new DefaultHealthCheck(
                    (context => context.AddCheckState(new HealthCheckResult("Lamda", false, new {Result = "bad"}))))
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthServer();
        }
    }
}