using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public class ConnectionInformation
    {
        #region Properties

        public string UserName { get; private set; }
        public string DomainName { get; private set; }
        public string EnvironmentName { get; private set; }
        public string ConnectionId { get; private set; }
        public string ClientAddress { get; private set; }
        public string ApplicationVersion { get; private set; }
        public string BrowserVersion { get; private set; }
        public DateTime ConnectedDate { get; private set; }

        #endregion Properties

        #region Constructors

        public ConnectionInformation(UserIdentity user, string connectionId, string clientAddress, string applicationVersion, string browserVersion)
            : this(user.UserName, user.DomainName, user.EnvironmentName, connectionId, clientAddress, applicationVersion, browserVersion) { }

        public ConnectionInformation(string userName, string domainName, string environmentName, string connectionId, string clientAddress, string applicationVersion, string browserVersion)
        {
            if (string.IsNullOrEmpty(userName)) throw new NullReferenceException("User Name is a required parameter!");

            UserName = userName;
            DomainName = domainName;
            EnvironmentName = environmentName;
            ConnectionId = connectionId;

            ClientAddress = clientAddress;
            ApplicationVersion = applicationVersion;
            BrowserVersion = browserVersion;

            ConnectedDate = DateTime.Now;
        }

        #endregion Constructors

        #region Methods

        public bool Equals(string userName)
        {
            // Null user implies all users
            return string.IsNullOrEmpty(userName) || UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Methods
    }
}
