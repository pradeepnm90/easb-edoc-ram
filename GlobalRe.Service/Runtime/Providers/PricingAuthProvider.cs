using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Runtime.Providers
{
    public class PricingAuthProvider : OAuthAuthorizationServerProvider
    {
        private static string EnvironmentName = MarkelConfiguration.EnvironmentName;
        private static IAuthenticationManager AuthenticationManager = new ADAuthenticationManager();
        private static IAuthorizationManager AuthorizationManager = new ERMSAuthorizationManager();

        #region Auth Managers

        private Dictionary<string, object> GetUserClaims(string user, string password)
        {
            string userName = GetUserName(user);
            string domainName = GetDomainName(user);

            Dictionary<string, object> userMetaData = new Dictionary<string, object>();
            userMetaData.Add("user", userName);
            userMetaData.Add("domainName", domainName);
            userMetaData.Add("userName", userName);
            userMetaData.Add("environment", EnvironmentName);
            userMetaData.Add("isAuthenticated", false);
            userMetaData.Add("isAuthorized", false);

            if (!AuthenticationManager.Authenticate(userName, password, domainName)) return userMetaData;
            userMetaData["isAuthenticated"] = true;

            UserIdentity userIdentity = AuthorizationManager.GetUserIdentity(userName, domainName, EnvironmentName, MarkelConfiguration.ApplicationName);

            if (!EnvironmentName.Equals(userIdentity?.EnvironmentName)) throw new Exception("User Identity does not match requested environment!");
            if (userIdentity == null) return userMetaData;

            // Successful Authentication
            userMetaData["isAuthorized"] = true;
            userMetaData.Add("application", userIdentity.ApplicationName);
            userMetaData.Add("applicationVersion", IOHelper.GetServerVersion());
            userMetaData.Add("authTokenDate", userIdentity.AuthenticatedOn.ToString());
            userMetaData.Add("authToken", userIdentity.AuthenticationToken.ToString());
            userMetaData.Add("authTokenExpireDate", userIdentity.AuthenticationTokenExpiration.ToString());
            userMetaData.Add("ermsHome", userIdentity.GetPath("ErmsHome"));
            userMetaData.Add("serviceAccount", userIdentity.IsServiceAccount.ToString());
            // this better match: userMetaData.Add("userName", userIdentity.UserName);
            foreach (var permission in userIdentity.Permissions.Where(p => p.Value == true))
            {
                userMetaData.Add(permission.Key, permission.Value.ToString());
            }

            return userMetaData;
        }

        #region Helper Methods

        private static string GetUserName(string user)
        {
            // Split user name from domain (if it exists)
            int index;
            if ((index = user.IndexOf("\\")) > 0)
            {
                return user.Substring(index + 1).ToUpper();
            }
            else if ((index = user.IndexOf("@")) > 0)
            {
                return user.Substring(0, index).ToUpper();
            }
            else
            {
                return user.ToUpper();
            }
        }

        private static string GetDomainName(string user)
        {
            // Split user name from domain (if it exists)
            int index;
            if ((index = user.IndexOf("\\")) > 0)
            {
                return user.Substring(0, index).ToUpper();
            }
            else if ((index = user.IndexOf("@")) > 0)
            {
                return user.Substring(index + 1).ToUpper();
            }
            else
            {
                // Default Domain Name
                return MarkelConfiguration.DomainName;
            }
        }

        #endregion Helper Methods

        #endregion Auth Managers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            if (context.Identity != null && context.Identity.Claims != null)
            {
                foreach (var claim in context.Identity.Claims)
                {
                    Type type = Type.GetType(claim.ValueType) ?? Type.GetType("System.String");
                    context.AdditionalResponseParameters.Add(claim.Type, Convert.ChangeType(claim.Value, type));
                }
            }
            return base.TokenEndpointResponse(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                if (!context.OwinContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
                {
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                }

                using (var ctx = new PrincipalContext(ContextType.Domain))
                {
                    Dictionary<string, object> userClaims = GetUserClaims(context.UserName, context.Password);
                    if ((bool)userClaims["isAuthenticated"])
                    {
                        // Convert to proper claims identity
                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        foreach (var claim in userClaims)
                        {
                            Type valueType = claim.Value.GetType();
                            string value = claim.Value != null ? claim.Value.ToString() : null;
                            identity.AddClaim(new Claim(claim.Key, value, valueType.ToString()));
                        }

                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", string.Format("The user name or password is incorrect. user:{0}", context.UserName));
                    }
                }
            });
        }
    }
}