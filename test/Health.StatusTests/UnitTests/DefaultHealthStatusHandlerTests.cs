using System;
using System.Collections.Generic;
using Health.Status.Handlers;
using Health.Status.HealthChecks;
using Health.Status.Models;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Health.StatusTests.UnitTests
{
    public class DefaultHealthStatusHandlerTests
    {
        [Fact]
        public void DefaultAction_Execute_Success()
        {
            var handler = new DefaultHealthStatusHandler(new List<IHealthStatusCheck>()
            {
                new DefaultHealthCheck()
            });

            var httpContext = new DefaultHttpContext();
            handler.Execute(httpContext).Wait();

            Assert.Equal(200, httpContext.Response.StatusCode);
        }

        [Fact]
        public void DefaultAction_Execute_Failed()
        {
            var handler = new DefaultHealthStatusHandler(new List<IHealthStatusCheck>()
            {
                new DefaultHealthCheck(
                    (context => context.AddCheckState(new HealthCheckResult("Lamda", false, new {result = "bad"}))))
            });

            var httpContext = new DefaultHttpContext();
           
            handler.Execute(httpContext).Wait();

            Assert.Equal(500, httpContext.Response.StatusCode);
        }

        [Fact]
        public void DefaultAction_Exception_Failed()
        {
            var handler = new DefaultHealthStatusHandler(new List<IHealthStatusCheck>()
            {
                new DefaultHealthCheck(
                    (context => { throw new Exception("Error");  }))
            });

            var httpContext = new DefaultHttpContext();

            handler.Execute(httpContext).Wait();

            Assert.Equal(500, httpContext.Response.StatusCode);
        }
    }
}