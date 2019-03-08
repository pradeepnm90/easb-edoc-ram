using Markel.Pricing.Service.Infrastructure.Models;

namespace Markel.Pricing.Service.Infrastructure.Security
{
    /// <summary>
    /// Authorization is the verification that the connection attempt is allowed. Authorization occurs after successful authentication.
    /// </summary>
    public interface IAuthorizationManager
    {
        UserIdentity GetUserIdentity(string loginName);
        UserIdentity GetUserIdentity(string userName, string domainName, string environmentName, string applicationName);
        ServiceToken GetServiceToken(string userName, string domainName, string environmentName, string applicationName);

        bool ValidateToken(string token, string environmentName);
    }
}
