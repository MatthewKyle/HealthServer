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
    public class DefaultStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthServer();
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