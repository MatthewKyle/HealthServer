using System;
using System.Threading.Tasks;
using Health.Status.Models;

namespace Health.Status.HealthChecks
{
    public class DefaultHealthCheck : IHealthStatusCheck
    {
        private readonly Action<IHealthContext> _checkFunc;
        private readonly string name;

        public DefaultHealthCheck()
        {
            name = GetType().Name;
            SetCheck();
        }

        public DefaultHealthCheck(Action<IHealthContext> checkFunc)
        {
            _checkFunc = checkFunc;

            name = GetType().Name;
            SetCheck();
        }

        public Action<IHealthContext> Check { get; set; }

        public Task Execute(IHealthContext context)
        {
            return Task.Factory.StartNew(() => Check.Invoke(context));
        }

        private void DefaultCheck(IHealthContext context)
        {
            context.AddCheckState(new HealthCheckResult(name, true));
        }

        private void SetCheck()
        {
            Check = _checkFunc ?? DefaultCheck;
        }
    }
}