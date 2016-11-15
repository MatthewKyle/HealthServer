using System;
using System.Threading.Tasks;

namespace Health.Status.Models
{
    public interface IHealthStatusCheck
    {
        Task Execute(IHealthContext context);
    }
}