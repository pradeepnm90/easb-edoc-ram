using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Providers
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        #region Constants

        public static OAuthBearerAuthenticationOptions BearerOptions { get; } = new OAuthBearerAuthenticationOptions();

        private const string PASSWORD_ENCODING = "PE";
        private const string X_REQUEST_ID = "X-Request-ID";
        private string EnvironmentName = MarkelConfiguration.EnvironmentName;
        private IAuthenticationManager AuthenticationManager = new ADAuthenticationManager();
        private IAuthorizationManager AuthorizationManager = new ERMSAuthorizationManager();
        private UserIdentity userIdentity;  // mp... 02/27/2019 - move up

        #endregion Constants

        #region Auth Managers

        private Dictionary<string, object> GetUserClaims(string user, string password, bool isTrustedServer)
        {
            string userName = GetUserName(user);
            string domainName = GetDomainName(user);
            if (string.IsNullOrWhiteSpace(userName)) throw new UnauthorizedAccessException("User Name is required for Authentication!");

            Dictionary<string, object> userMetaData = new Dictionary<string, object>();
            userMetaData.Add("user", userName);
            userMetaData.Add("domainName", domainName);
            userMetaData.Add("userName", userName);
            userMetaData.Add("environment", EnvironmentName);
            userMetaData.Add("isAuthenticated", false);
            userMetaData.Add("isAuthorized", false);

            if (!isTrustedServer && !AuthenticationManager.Authenticate(userName, password, domainName)) return userMetaData;
            userMetaData["isAuthenticated"] = true;

            userIdentity = AuthorizationManager.GetUserIdentity(userName, domainName, EnvironmentName, MarkelConfiguration.ApplicationName);

            if (!EnvironmentName.Equals(userIdentity?.EnvironmentName)) throw new Exception("User Identity does not match requested environment!");
            if (userIdentity == null) return userMetaData;

            // Successful Authentication
            userMetaData["isAuthorized"] = true;
            userMetaData.Add("application", userIdentity.ApplicationName);
            userMetaData.Add("applicationVersion", IOHelper.GetServerVersion());

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
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password".
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                if (!context.OwinContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
                {
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                }

                try
                {
                    string requestId = context.Request.Headers[X_REQUEST_ID];
                    bool isTrustedServer = IsTrustedServer(context.Request.Headers["Client-Secret"]);

                    bool isPasswordEncoded = "Base64".Equals(context.Request.Headers[PASSWORD_ENCODING]);
                    string password = isPasswordEncoded ? context.Password.Base64Decode() : context.Password;

                    Dictionary<string, object> userClaims = GetUserClaims(context.UserName, password, isTrustedServer);
                    userClaims.Add("apiUrl", context.Request.Uri.AbsoluteUri.Replace(context.Request.Uri.AbsolutePath, ""));
                    userClaims.Add("personId", userIdentity.NameId.ToString());  // mp... 02/27/2019
                    userClaims.Add("userId", userIdentity.UserId.ToString());  // mp... 02/27/2019

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

                        if (!string.IsNullOrEmpty(requestId))
                        {
                            context.OwinContext.Response.Headers.Add(X_REQUEST_ID, new string[] { requestId });
                        }

                        context.Validated(identity);
                    }
                    else
                    {
                         context.SetError("invalid_grant", $"User '{context.UserName}' is not Authorized for this application!");
                    }
                }
                catch (UnauthorizedAPIException ex)
                {
                    context.SetError("invalid_grant", ex.Message);
                }
            });
        }

        private static bool IsTrustedServer(string encrptedClientSecret)
        {
            if (string.IsNullOrEmpty(encrptedClientSecret)) return false;

            Dictionary<string, string> secretDetails = encrptedClientSecret.DecryptJson<AesManaged, Dictionary<string, string>>(MarkelConfiguration.EnvironmentName);
            string machine = secretDetails?["Machine"];
            string environment = secretDetails?["Environment"];

            if (!MarkelConfiguration.EnvironmentName.Equals(environment)) return false;

            Regex regex = new Regex(MarkelConfiguration.TrustedServers);
            Match match = regex.Match(machine);
            return match.Success;
        }
    }
}