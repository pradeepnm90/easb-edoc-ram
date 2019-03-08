using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CoreVelocity.Core.Security
{
    /// <summary>
    /// Represents implementation of IEnterpriseIdentity, purpose - ERMS authentication
    /// </summary>
    [Serializable]
    public class EnterpriseIdentity
    {
        /// <summary>
        /// Default parameterless constructor
        /// </summary>
        public EnterpriseIdentity()
        {
            connectionsList = new List<ApplicationConnector>();
        }

        #region Protected Members

        /// <summary>
        /// Lock and synchronization object for multithreaded operations
        /// </summary>
        protected object syncRoot = new object();
        protected List<ApplicationConnector> connectionsList;

        #endregion

        #region Private Members

        /// <summary>
        /// Backing field for Permissionset.
        /// </summary>
        private PermissionSet permissionSet;
        /// <summary>

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the application using the Enterprise Identity.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the Windows Domain name for authentication
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the Windows user name for authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Environment for authentication
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the Authentication Token when a user is successfully authenticated with ERMS
        /// </summary>
        public Guid? AuthenticationToken { get; set; }

        /// <summary>
        /// Gets or sets the Authentication Token authentication time
        /// </summary>
        public DateTime AuthenticatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Authentication Token Expiry time
        /// </summary>
        public DateTime AuthenticationTokenExpires { get; set; }

        /// <summary>
        /// Gets or sets the boolean value based on the user authentication
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the NameId of the Authenticated user. This is used for authorization
        /// </summary>
        public int NameId { get; set; }

        /// <summary>
        /// Gets or sets the UserId of the Authenticated user.
        /// This is required as some queries in ERMS use the UserId instead of the NameId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets the EXE Location path for the environment from ERMS
        /// </summary>
        public string ErmsHomePath { get; set; }

        /// <summary>
        /// Gets or sets Impersonated By user information
        /// </summary>
        public string ImpersonatedBy { get; set; }

        /// <summary>
        /// Gets or sets if a Service Account is being used for Authentication
        /// </summary>
        public bool IsServiceAccount { get; set; }

        /// <summary>
        /// Gets or sets the Authentication Provider used to authenticate and generate identity
        /// </summary>
        public string AuthenticatedBy { get; set; }

        /// <summary>
        /// Gets or sets the PermissionSet for an authenticated user
        /// </summary>
        public PermissionSet ErmsPermissionSet
        {
            get
            {
                if (permissionSet == null)
                {
                    permissionSet = new PermissionSet();
                }
                return permissionSet;
            }
            set
            {
                permissionSet = value;
            }
        }

        /// <summary>
        /// List of connections with the connectionstrings
        /// </summary>
        public List<ApplicationConnector> Connections
        {
            get
            {
                return connectionsList;
            }
            set
            {
                connectionsList = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new ApplicationConnector entry to the list of available connection strings
        /// </summary>
        /// <param name="connection"></param>
        public void AddConnection(ApplicationConnector connection)
        {
            lock (syncRoot)
            {
                connectionsList.Add(connection);
            }
        }

        /// <summary>
        /// Clears all connection strings that are marked IsServersideOnly = true
        /// </summary>
        public void CleanseForClient()
        {
            lock (syncRoot)
            {
                connectionsList.RemoveAll(c => c.IsServersideOnly == true);
            }
        }

        #endregion

    }
}
