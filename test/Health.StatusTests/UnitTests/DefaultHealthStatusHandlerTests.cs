namespace Health.StatusTests.UnitTests
{
    using System;
    using System.Collections.Generic;

    using HealthServer.Configuration.DependencyInjection.Options;
    using HealthServer.Handlers;
    using HealthServer.HealthChecks;
    using HealthServer.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    using Xunit;

    public class DefaultHealthStatusHandlerTests
    {
        [Fact]
        public void DefaultAction_Exception_Failed()
        {
            var handler =
                new DefaultHealthStatusHandler(
                    new List<IHealthStatusCheck>
                        {
                            new DefaultHealthCheck(context => { throw new Exception("Error"); })
                        },
                    new OptionsWrapper<HealthServerHandlerOptions>(new HealthServerHandlerOptions()));

            var httpContext = new DefaultHttpContext();

            handler.Execute(httpContext).Wait();

            Assert.Equal(500, httpContext.Response.StatusCode);
        }

        [Fact]
        public void DefaultAction_Execute_Failed()
        {
            var handler =
                new DefaultHealthStatusHandler(
                    new List<IHealthStatusCheck>
                        {
                            new DefaultHealthCheck(
                                context =>
                                        context.AddCheckState(new HealthCheckResult("Lamda", false, new { result = "bad" })))
                        },
                    new OptionsWrapper<HealthServerHandlerOptions>(new HealthServerHandlerOptions()));

            var httpContext = new DefaultHttpContext();

            handler.Execute(httpContext).Wait();

            Assert.Equal(500, httpContext.Response.StatusCode);
        }

        [Fact]
        public void DefaultAction_Execute_Success()
        {
            var handler = new DefaultHealthStatusHandler(
                              new List<IHealthStatusCheck> { new DefaultHealthCheck() },
                              new OptionsWrapper<HealthServerHandlerOptions>(new HealthServerHandlerOptions()));

            var httpContext = new DefaultHttpContext();
            handler.Execute(httpContext).Wait();

            Assert.Equal(200, httpContext.Response.StatusCode);
        }
    }
}