namespace HealthServer.HealthChecks
{
    using System;
    using System.Threading.Tasks;

    using HealthServer.Models;

    public class DefaultHealthCheck : IHealthStatusCheck
    {
        private readonly Action<IHealthContext> _checkFunc;
        private readonly string name;

        public DefaultHealthCheck()
        {
            this.name = this.GetType().Name;
            this.SetCheck();
        }

        public DefaultHealthCheck(Action<IHealthContext> checkFunc)
        {
            this._checkFunc = checkFunc;

            this.name = this.GetType().Name;
            this.SetCheck();
        }

        public Action<IHealthContext> Check { get; set; }

        public Task Execute(IHealthContext context)
        {
            return Task.Factory.StartNew(() => this.Check.Invoke(context));
        }

        public string Name => name;

        private void DefaultCheck(IHealthContext context)
        {
            context.AddCheckState(new HealthCheckResult(this.name, true));
        }

        private void SetCheck()
        {
            this.Check = this._checkFunc ?? this.DefaultCheck;
        }
    }
}