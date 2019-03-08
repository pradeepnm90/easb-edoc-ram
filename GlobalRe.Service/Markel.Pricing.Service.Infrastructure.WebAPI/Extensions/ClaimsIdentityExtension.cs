using System.Linq;
using System.Security.Claims;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class ClaimsIdentityExtension
    {
        public static Claim GetClaim(this ClaimsIdentity identity, string claim)
        {
            return identity?.Claims?.FirstOrDefault(c => string.Equals(c.Type, claim));
        }

        public static string GetClaimValue(this ClaimsIdentity identity, string claim)
        {
            return identity?.GetClaim(claim)?.Value;
        }
    }
}
