using Markel.Pricing.Service.Infrastructure.Models;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    public interface IUserManager
    {
        UserIdentity UserIdentity { get; }
        ServiceToken GetServiceToken(string serviceName);
        bool IsBackgroundProcess { get; }

        void Initialize(string employeeLoginId);
    }
}
