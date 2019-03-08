using System;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    public class ServiceToken
    {
        #region Public Properties

        public string AuthenticationToken { get; private set; }

        public DateTime? AuthenticatedOn { get; private set; }

        public DateTime? AuthenticationTokenExpiration { get; private set; }

        #endregion Public Properties

        #region Constructor

        public ServiceToken(string authenticationToken, DateTime authenticatedOn, DateTime? authenticationTokenExpiration)
        {
            AuthenticationToken = authenticationToken;
            AuthenticatedOn = authenticatedOn;
            AuthenticationTokenExpiration = authenticationTokenExpiration;
        }

        #endregion Constructor
    }
}
