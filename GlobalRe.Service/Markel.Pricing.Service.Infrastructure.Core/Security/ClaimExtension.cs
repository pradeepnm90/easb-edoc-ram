using Markel.Pricing.Service.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Markel.Pricing.Service.Infrastructure.Security
{
    public static class ClaimExtension
    {
        public static T GetUserClaim<T>(this IPrincipal user, string type)
        {
            IEnumerable<Claim> claims = ((System.Security.Claims.ClaimsPrincipal)user).Claims;
            Claim claim = claims.SingleOrDefault(c => c.Type == type);

            if (claim != null)
            {
                Type valueType = Type.GetType(claim.ValueType) ?? Type.GetType("string");
                if (typeof(T) == valueType)
                {
                    return (T)Convert.ChangeType(claim.Value, valueType);
                }

                throw new InvalidCastException(string.Format("User Claim '{0}' is type '{1}', expected '{2}", type, valueType.ToString(), typeof(T).ToString()));
            }

            return default(T);
        }

        public static IPrincipal Validate(this IPrincipal user)
        {
            if (!user.IsAuthenticated() || !user.IsAuthorized())
                throw new UnauthorizedAPIException($"User '{user.UserName()}' is not Authorized in '{user.ApplicationName()}'!");
            return user;
        }

        public static bool IsAuthenticated(this IPrincipal user)
        {
            return user.GetUserClaim<bool>("isAuthenticated");
        }

        public static bool IsAuthorized(this IPrincipal user)
        {
            return user.GetUserClaim<bool>("isAuthorized");
        }

        public static string User(this IPrincipal user)
        {
            return user.GetUserClaim<string>("user");
        }

        public static string UserName(this IPrincipal user)
        {
            return user.GetUserClaim<string>("userName");
        }

        public static string DomainName(this IPrincipal user)
        {
            return user.GetUserClaim<string>("domainName");
        }

        public static string EnvironmentName(this IPrincipal user)
        {
            return user.GetUserClaim<string>("environment");
        }

        public static string ApplicationName(this IPrincipal user)
        {
            return user.GetUserClaim<string>("application");
        }
    }
}
