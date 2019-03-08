using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    public class IdentityContainer
    {
        #region Public Properties

        public UserIdentity UserIdentity { get; private set; }

        private Dictionary<string, ServiceToken> ServiceTokens = new Dictionary<string, ServiceToken>();

        #endregion Public Properties

        #region Constructor

        public IdentityContainer(UserIdentity userIdentity)
        {
            UserIdentity = userIdentity;
        }

        #endregion Constructor

        #region Methods

        public void SetToken(string serviceName, ServiceToken serviceToken)
        {
            ServiceTokens[serviceName] = serviceToken;
        }

        public ServiceToken GetToken(string serviceName)
        {
            if (!ServiceTokens.ContainsKey(serviceName)) return null;
            return ServiceTokens[serviceName];
        }

        #endregion Methods
    }
}
