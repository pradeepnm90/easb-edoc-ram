using Markel.Pricing.Service.Infrastructure.Config;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    public class UserIdentity
    {
        #region Private Properties

        private Dictionary<string, string> Connections { get; set; }

        #endregion Private Properties

        #region Public Properties

        public int UserId { get; private set; }

        public int NameId { get; private set; }

        public Dictionary<string, bool> Permissions { get; private set; }

        public Dictionary<string, string> Paths { get; private set; }

        public string ApplicationName { get; private set; }
        
        public string UserName { get; private set; }

        public string DomainName { get; private set; }

        public string EmployeeLoginID { get { return $"{UserName}@{DomainName}"; } }

        public string EnvironmentName { get; private set; }
        public DateTime AuthenticatedOn { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public string ImpersonatedBy { get; private set; }

        public bool IsServiceAccount { get; private set; }

        #endregion

        #region Constructor

        public UserIdentity(string applicationName, int userId, int nameId, string userName, string domainName, string environmentName,
                            DateTime authenticatedOn, bool isAuthenticated, string impersonatedBy, bool isServiceAccount,
                            Dictionary<string, bool> permissions,
                            Dictionary<string, string> connections,
                            Dictionary<string, string> paths)
        {
            ApplicationName = applicationName;
            UserId = userId;
            NameId = nameId;
            UserName = userName;
            DomainName = domainName;
            EnvironmentName = environmentName;

            AuthenticatedOn = authenticatedOn;
            IsAuthenticated = isAuthenticated;
            ImpersonatedBy = impersonatedBy;
            IsServiceAccount = isServiceAccount;

            Permissions = permissions ?? new Dictionary<string, bool>();
            Connections = connections ?? new Dictionary<string, string>();
            Paths = paths ?? new Dictionary<string, string>();
        }

        #endregion Constructor

        #region Public Methods

        public string GetPath(string pathKey)
        {
            if (Paths.ContainsKey(pathKey))
            {
                return Paths[pathKey];
            }

            return null;
        }

        public string GetConnectionString(string connectionKey = "")
        {
            if (string.IsNullOrEmpty(connectionKey)) connectionKey = MarkelConfiguration.ApplicationName;
            if (Connections.ContainsKey(connectionKey))
            {
                return Connections[connectionKey];
            }

            return null;
        }

        #endregion Public Methods
    }
}
