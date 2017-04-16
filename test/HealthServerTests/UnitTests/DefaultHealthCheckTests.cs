using System;
using HealthServer.HealthChecks;
using HealthServer.Models;
using Xunit;

namespace HealthServerTests.UnitTests
{
    public class DefaultHealthCheckTests
    {
        [Fact]
        public void DefaultAction_EmptyConstructor_Success()
        {
            var healthContext = new HealthContext();
            var defualtCheck = new DefaultHealthCheck();

            defualtCheck.Execute(healthContext).Wait();

            Assert.False(healthContext.Response.IsFailed);
            Assert.Equal(typeof(DefaultHealthCheck).Name, healthContext.Response.Results[0].Name);
        }

        [Fact]
        public void DefaultAction_Lamda_Constructor_Success()
        {
            var healthContext = new HealthContext();
            var defualtCheck =
                new DefaultHealthCheck(
                    context => context.AddCheckState(new HealthCheckResult("Lamda", false, new { result = "bad" })));

            defualtCheck.Execute(healthContext).Wait();

            Assert.True(healthContext.Response.IsFailed);
            Assert.Equal("Lamda", healthContext.Response.Results[0].Name);
            Assert.False(healthContext.Response.Results[0].IsSuccess);
            Assert.NotNull(healthContext.Response.Results[0].Response);
        }

        [Fact]
        public void DefaultAction_Lamda_Throws_Fail()
        {
            var healthContext = new HealthContext();
            var defualtCheck = new DefaultHealthCheck(context => { throw new Exception("Error"); });

            Assert.ThrowsAny<Exception>(() => defualtCheck.Execute(healthContext).Wait());
        }

        [Fact]
        public void DefaultAction_Null_Constructor_Success()
        {
            var healthContext = new HealthContext();
            var defualtCheck = new DefaultHealthCheck(null);

            defualtCheck.Execute(healthContext).Wait();

            Assert.False(healthContext.Response.IsFailed);
            Assert.Equal(typeof(DefaultHealthCheck).Name, healthContext.Response.Results[0].Name);
        }
    }
}