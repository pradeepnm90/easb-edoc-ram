using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.Pricing.Service.Infrastructure.Security
{
    public class UserManager : IUserManager
    {
        #region Member Variables

        private ICacheStoreManager _cacheStoreManager;
        private IdentityContainer _identityContainer = null;

        #endregion Member Variables

        #region Constructor

        public UserManager(ICacheStoreManager cacheStoreManager)
        {
            _cacheStoreManager = cacheStoreManager;
            IsBackgroundProcess = false;
        }

        #endregion Constructor

        #region Properties

        public bool IsBackgroundProcess { get; private set; }

        public UserIdentity UserIdentity
        {
            get
            {
                return IdentityContainer.UserIdentity;
            }
        }

        public ServiceToken GetServiceToken(string serviceName)
        {
            IAuthorizationManager authorizationManager = new ERMSAuthorizationManager();

            // Initialize or Validate Token. If expired, refresh.
            IdentityContainer identityContainer = IdentityContainer;
            UserIdentity user = identityContainer.UserIdentity;
            ServiceToken serviceToken = identityContainer.GetToken(serviceName);

            if (string.Equals(serviceName, "ERMS") && !authorizationManager.ValidateToken(serviceToken?.AuthenticationToken, user.EnvironmentName))
            {
                serviceToken = authorizationManager.GetServiceToken(user.UserName, user.DomainName, user.EnvironmentName, user.ApplicationName);
                identityContainer.SetToken(serviceName, serviceToken);
                UpdateIdentityContainer(identityContainer);
            }

            return serviceToken;
        }

        #endregion Properties

        #region Methods

        public void Initialize(string employeeLoginId)
        {
            if (_identityContainer == null)
            {
                IsBackgroundProcess = true;

                string[] id = employeeLoginId.Split('@');
                if (id.Length != 2) throw new NotAllowedAPIException($"Employee Login ID requires two parts ([user]@[domain]): {employeeLoginId}");

                _identityContainer = GetIdentityContainer(id[0], id[1], MarkelConfiguration.EnvironmentName, MarkelConfiguration.ApplicationName);

                if (!_identityContainer.UserIdentity.IsAuthenticated)
                    throw new UnauthorizedAPIException($"User '{employeeLoginId}' is not Authorized!");
            }
        }

        #endregion Methods

        #region Private Methods

        private IdentityContainer IdentityContainer
        {
            get
            {
                // Validate User Identity from Context, or use current identity
                // Current Identity is set when running asynchronous tasks
                IdentityContainer identityContainer = GetUserIdentityFromHttpContext() ?? _identityContainer;
                if (identityContainer == null) throw new UnauthorizedAPIException("User Context was not set!");
                return identityContainer;
            }
        }

        private IdentityContainer GetUserIdentityFromHttpContext()
        {
            // Get User Identity From Http Context
            var currentContext = System.Web.HttpContext.Current;
            if (currentContext?.User?.UserName() != null)
            {
                // User Info from OAuth Claims
                var currentUser = currentContext.User.Validate();
                string userName = currentUser.UserName();
                string environmentName = currentUser.EnvironmentName();

                if (!MarkelConfiguration.EnvironmentName.Equals(environmentName))
                    throw new NotAllowedAPIException($"Invalid Auth Token for User '{userName}' in '{environmentName}'! Expected '{MarkelConfiguration.EnvironmentName}'");

                if (_identityContainer == null)
                {
                    string domainName = currentUser.DomainName();
                    string applicationName = currentUser.ApplicationName();

                    _identityContainer = GetIdentityContainer(userName, domainName, environmentName, applicationName);
                }

                if (!userName.Equals(_identityContainer?.UserIdentity?.UserName)) throw new Exception($"UserManager: User Name in HttpContext ({userName}) does not match current User Identity ({_identityContainer?.UserIdentity?.UserName})!");

                return _identityContainer;
            }

            return null;
        }

        private IdentityContainer GetIdentityContainer(string userName, string domainName, string environmentName, string applicationName)
        {
            if (!MarkelConfiguration.EnvironmentName.Equals(environmentName))
                throw new NotAllowedAPIException($"Invalid Request! User '{userName}' is attempting to log into '{environmentName}'. Server is configured for '{MarkelConfiguration.EnvironmentName}' only!");

            string cacheKey = _cacheStoreManager.BuildKey("UserIdentity", userName);
            IdentityContainer cachedIdentityContainer = _cacheStoreManager.GetItem<IdentityContainer>(cacheKey, (action) =>
            {
                try
                {
                    IAuthorizationManager authorizationManager = new ERMSAuthorizationManager();
                    UserIdentity userIdentity = authorizationManager.GetUserIdentity(userName, domainName, environmentName, applicationName);
                    if (userIdentity != null)
                    {
                        // Auth Token expires in one year, or when API is restarted
                        return new CacheItem(MarkelConfiguration.AccessTokenLifetime, null, new IdentityContainer(userIdentity));
                    }

                    return new CacheItem();
                }
                catch (Exception)
                {
                    string message = string.Format("Authentication failed for: Application: {0}, User: {1}, Domain: {2}, Environment: {3}", applicationName, userName, domainName, environmentName);
                    throw new UnauthorizedAPIException(message);
                }
            }, false);

            return cachedIdentityContainer;
        }

        private void UpdateIdentityContainer(IdentityContainer identityContainer)
        {
            string cacheKey = _cacheStoreManager.BuildKey("UserIdentity", identityContainer.UserIdentity.UserName);
            _cacheStoreManager.Update(cacheKey, identityContainer, false);
        }

        #endregion Private Methods
    }
}
