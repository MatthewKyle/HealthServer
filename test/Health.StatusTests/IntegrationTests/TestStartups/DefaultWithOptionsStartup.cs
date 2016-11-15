namespace HealthServerTests.IntegrationTests.TestStartups
{
    using HealthServer.Configuration;
    using HealthServer.Configuration.DependencyInjection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class DefaultWithOptionsStartup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthServer();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthServer((options => options.DefaultHandlerRoute = "/healthz"));
        }
    }
}