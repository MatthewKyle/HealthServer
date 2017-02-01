namespace HealthServerTests.IntegrationTests.TestStartups
{
    using System.Collections.Generic;

    using HealthServer.Configuration;
    using HealthServer.Configuration.DependencyInjection;
    using HealthServer.Configuration.DependencyInjection.BuilderExtensions;
    using HealthServer.HealthChecks;
    using HealthServer.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class CheckFailsStarup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthServer();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthServer()
                .AddHealthChecks(
                    new List<IHealthStatusCheck>
                        {
                            new DefaultHealthCheck(),
                            new DefaultHealthCheck(
                                context =>
                                        context.AddCheckState(new HealthCheckResult("Lamda", false, new { Result = "bad" })))
                        });
        }
    }
}