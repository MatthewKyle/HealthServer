using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health.Status.Configuration;
using Health.Status.Configuration.DependencyInjection;
using Health.Status.Configuration.DependencyInjection.BuilderExtensions;
using Health.Status.HealthChecks;
using Health.Status.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestHost
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthServer().AddHealthChecks(new List<IHealthStatusCheck>()
            {
                new DefaultHealthCheck(),
                new DefaultHealthCheck((context)=> context.AddCheckState(new HealthCheckResult("LamdaCheck", false, new { state="bad things happened."})))
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
