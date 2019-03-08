using CoreVelocity.Core.Security;
using CoreVelocity.Security.Model;
using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.ERMSAuthentication;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Security
{
    public class ERMSAuthorizationManager : IAuthorizationManager
    {
        private enum ApplicationGroup
        {
            Pricing,
            GRS
        }
        #region Permissions
        private enum PermissionEnum
        {
            NONE,
            CAN_EDIT_PRICING,
            CAN_CREATE_PRICING,
            CAN_VIEW_PRICING,
            CAN_COMMENT_ON_PRICING,
            CAN_APPROVE_PRICING,
            CAN_UNAPPROVE_PRICING,
            CAN_EXCLUDE_FROM_RATE_CHANGE,
            CAN_OPEN_IR,
            CAN_OPEN_WORKSITE,
            CAN_OPEN_ETWO,
            CAN_OPEN_ERMS,
            CAN_CREATE_NEWDEAL,
            CAN_FLUSH_PRICING_CACHES,
            ACCESS_PRICING_WEB_MODELS
        }

        private enum PermissionEnumGRS
        {
            NONE,
            UNDERWRITING_DEALPIPELINE_EDITDEAL,
            UNDERWRITING_DEALEDIT_EDITDEAL
        }

        private readonly static Dictionary<PermissionEnumGRS, string> PermissionLookupGRS = new Dictionary<PermissionEnumGRS, string>()
        {
            { PermissionEnumGRS.UNDERWRITING_DEALPIPELINE_EDITDEAL, "Underwriting DealPipeline EditDeal" },
            { PermissionEnumGRS.UNDERWRITING_DEALEDIT_EDITDEAL, "Underwriting DealEdit EditDeal" }
        };
        /*
        private static List<string> AllPermissionDescriptions
        {
            get
            {
                List<string> permissions = new List<string>();

                foreach (PermissionEnum permission in Enum.GetValues(typeof(PermissionEnum)))
        */

        private readonly static Dictionary<PermissionEnum, string> PermissionLookup = new Dictionary<PermissionEnum, string>()
        {
            { PermissionEnum.CAN_EDIT_PRICING, "Can Edit Pricing" },
            { PermissionEnum.CAN_CREATE_PRICING, "Can Create Pricing" },
            { PermissionEnum.CAN_VIEW_PRICING, "Can View Pricing" },
            { PermissionEnum.CAN_COMMENT_ON_PRICING, "Can Comment on Pricing" },
            { PermissionEnum.CAN_APPROVE_PRICING, "Can Approve Pricing" },
            { PermissionEnum.CAN_UNAPPROVE_PRICING, "Can Unapprove Pricing" },
            { PermissionEnum.CAN_EXCLUDE_FROM_RATE_CHANGE, "Can Exclude From Rate Change" },
            { PermissionEnum.CAN_OPEN_IR, "Can Open Image Right" },
            { PermissionEnum.CAN_OPEN_WORKSITE, "Can Open Worksite" },
            { PermissionEnum.CAN_OPEN_ETWO, "Can Open e2" },
            { PermissionEnum.CAN_OPEN_ERMS, "Can Open ERMS" },
            { PermissionEnum.CAN_CREATE_NEWDEAL, "Can Create Deal" },
            { PermissionEnum.CAN_FLUSH_PRICING_CACHES, "Can Flush Pricing Caches" },
            { PermissionEnum.ACCESS_PRICING_WEB_MODELS, "Access PricingWeb Models" }
        };

        private static List<string> AllPermissionDescriptions
        {
            get
            {
                List<string> permissions = new List<string>();

                foreach (PermissionEnum permission in Enum.GetValues(typeof(PermissionEnum)))
                {
                    // Ignore NONE
                    if (permission != PermissionEnum.NONE)
                    {
                        var description = PermissionLookup[permission];
                        if (string.IsNullOrEmpty(description))
                        {
                            throw new Exception(string.Format(CultureInfo.InvariantCulture, "Permission '{0}' is missing a definition in PermissionHelper!", permission));
                        }

                        permissions.Add(description);
                    }
                }
                return permissions;
            }
        }

        private static List<string> AllPermissionDescriptionsGRS
        {
            get
            {
                List<string> permissions = new List<string>();

                foreach (PermissionEnumGRS permission in Enum.GetValues(typeof(PermissionEnumGRS)))
                {
                    // Ignore NONE
                    if (permission != PermissionEnumGRS.NONE)
                    {
                        var description = PermissionLookupGRS[permission];
                        if (string.IsNullOrEmpty(description))
                        {
                            throw new Exception(string.Format(CultureInfo.InvariantCulture, "Permission '{0}' is missing a definition in PermissionHelper!", permission));
                        }

                        permissions.Add(description);
                    }
                }
                return permissions;
            }
        }

        #endregion Permissions

        #region Properties

        private static AuthenticationServiceClient AuthenticationService = new AuthenticationServiceClient();

        #endregion Properties

        #region Authentication

        /// <summary>
        /// Authorizatizes the specified user via ERMS authentication.
        /// </summary>
        /// <param name="loginName">Employee Login ID ([user]@[domain])</param>
        /// <returns>User Identity</returns>
        public UserIdentity GetUserIdentity(string loginName)
        {
            string[] loginAccountName = loginName.Split('@');
            if (loginAccountName.Length != 2) throw new NotAllowedAPIException($"Employee Login ID requires two parts ([user]@[domain]): {loginName}");

            string userName = loginAccountName[0];
            string domainName = loginAccountName[1];

            return GetUserIdentity(userName, domainName, MarkelConfiguration.EnvironmentName, MarkelConfiguration.ApplicationName);
        }

        /// <summary>
        /// Authorizatizes the specified user via ERMS authentication.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <param name="applicationName">Name of the applicaiton.</param>
        /// <returns>User Identity</returns>
        public UserIdentity GetUserIdentity(string userName, string domainName, string environmentName, string applicationName)
        {
            IList<Permission> permissions;

            ApplicationGroup currentAppGroup;
            string appName = ConfigurationManager.AppSettings.Get("AppName");
            if (!String.IsNullOrEmpty(appName))
            {
                if (appName.ToLower() == "erms" || appName.ToLower() == "grs")
                    currentAppGroup = ApplicationGroup.GRS;
                else
                    currentAppGroup = ApplicationGroup.Pricing;
            }
            else
                currentAppGroup = ApplicationGroup.Pricing;

            if (currentAppGroup == ApplicationGroup.GRS)
            {
                permissions = AllPermissionDescriptionsGRS.Select(permission => new Permission() { FactDescription = permission }).ToList();
            }
            else
            {
                permissions = AllPermissionDescriptions.Select(permission => new Permission() { FactDescription = permission }).ToList();
            }

            try
            {
                EnterpriseIdentity enterpriseIdentity = AuthenticationService.Authenticate(new EnterpriseIdentity()
                {
                    ApplicationName = applicationName,
                    Username = userName,
                    DomainName = domainName,
                    EnvironmentName = environmentName,
                    ErmsPermissionSet = new PermissionSet(permissions, null)
                });

                return Transform(enterpriseIdentity);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAPIException(ex.Message);
            }
        }

        public ServiceToken GetServiceToken(string userName, string domainName, string environmentName, string applicationName)
        {
            try
            {
                AuthenticationResponse authResponse = AuthenticationService.AuthenticateWithParams(userName, domainName, environmentName, applicationName);
                if (!authResponse.IsAuthenticated)
                    throw new UnauthorizedAPIException(authResponse.AuthenticationError.Message);

                return new ServiceToken(
                    authenticationToken: authResponse.AuthenticationToken.ToString(),
                    authenticatedOn: DateTime.Now,
                    authenticationTokenExpiration: authResponse.AuthenticationTokenExpires
                );
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAPIException(ex.Message);
            }
        }

        public bool ValidateToken(string token, string environmentName)
        {
            if (string.IsNullOrEmpty(token)) return false;
            if (!MarkelConfiguration.EnvironmentName.Equals(environmentName)) return false;

            try
            {
                return AuthenticationService.ValidateAuthenticationTokenWithParams(token, environmentName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Authentication

        #region Helper Functions

        private static UserIdentity Transform(EnterpriseIdentity enterpriseIdentity)
        {
            if (enterpriseIdentity != null)
            {
                Dictionary<string, bool> authenticatedPermissions = new Dictionary<string, bool>();
                foreach (var permission in enterpriseIdentity.ErmsPermissionSet.Permissions)
                {
                    // NOTE: FactValue is a string. Booleans were converted to 0/1 so we need to handle that case for Permission Fact's (Start With 'Can').
                    if (permission.FactDescription.StartsWith("Can") && (permission.FactValue.Equals("0") || permission.FactValue.Equals("1")))
                    {
                        authenticatedPermissions.Add(permission.FactDescription, permission.FactValue);
                    }
                    else
                    {
                        authenticatedPermissions.Add(permission.FactDescription, permission.FactValue);
                    }
                }

                Dictionary<string, string> connections = new Dictionary<string, string>();
                foreach (var connection in enterpriseIdentity.Connections)
                {
                    connections.Add(connection.ConnectionKey, connection.ConnectionString);
                }

                Dictionary<string, string> paths = new Dictionary<string, string>();
                paths.Add("ErmsHome", enterpriseIdentity.ErmsHomePath);

                UserIdentity userIdentity = new UserIdentity(
                    enterpriseIdentity.ApplicationName,
                    enterpriseIdentity.UserId,
                    enterpriseIdentity.NameId,
                    enterpriseIdentity.Username,
                    enterpriseIdentity.DomainName,
                    enterpriseIdentity.EnvironmentName,
                    enterpriseIdentity.AuthenticatedOn,
                    enterpriseIdentity.IsAuthenticated,
                    enterpriseIdentity.ImpersonatedBy,
                    enterpriseIdentity.IsServiceAccount,
                    authenticatedPermissions,
                    connections,
                    paths
                );

                return userIdentity;
            }

            return default(UserIdentity);
        }

        #endregion Helper Functions
    }
}
