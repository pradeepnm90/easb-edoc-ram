using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using System;
using System.DirectoryServices.AccountManagement;

namespace Markel.Pricing.Service.Infrastructure.Security
{
    public class ADAuthenticationManager : IAuthenticationManager
    {
        public ADAuthenticationManager() { }

        public bool Authenticate(string userName, string password, string domainName)
        {
            // TODO: Security Review > Skip Authentication if Test User and Password is not supplied. Not valid for PROD.
            if (!MarkelConfiguration.EnvironmentName.Equals("PROD"))
            {
                if ("PRICING-TEST".Equals(userName, StringComparison.CurrentCultureIgnoreCase) && string.IsNullOrEmpty(password)) return true;
            }

            if (string.IsNullOrEmpty(password)) throw new UnauthorizedAPIException("Password is required!");

            try
            {
                // create a "principal context" - e.g. your domain (could be machine, too)
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domainName))
                {
                    // validate the credentials
                    bool isValid = pc.ValidateCredentials(userName, password);
                    return isValid;
                }
            }
            catch (Exception)
            {
                throw new UnauthorizedAPIException("Active Directory Authentication failed!");
            }
        }
    }
}
