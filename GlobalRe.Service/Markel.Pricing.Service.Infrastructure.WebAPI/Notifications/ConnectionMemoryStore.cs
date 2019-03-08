using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    internal class ConnectionMemoryStore : IConnectionStore
    {
        #region Private Variables

        private static object _connectedSessionLock = new object();
        private static List<ConnectionInformation> _connectedSessions = new List<ConnectionInformation>();

        #endregion Private Variables

        #region Constructor

        public ConnectionMemoryStore(IUserManager userManager) { }

        #endregion Constructor

        #region Public Methods

        public List<ConnectionInformation> GetAllConnections()
        {
            return GetConnections();
        }

        // NOTE: This method may not be thread safe. Consider another implementation.
        public List<ConnectionInformation> GetConnections(string userName = null)
        {
            lock (_connectedSessionLock)
            {
                if (string.IsNullOrEmpty(userName))
                    return _connectedSessions;

                return _connectedSessions.Where(connection => connection.Equals(userName)).ToList();
            }
        }

        public void Connect(ConnectionInformation connectionInformation)
        {
            lock (_connectedSessionLock)
            {
                if (!_connectedSessions.Exists(connection => connection.ConnectionId.Equals(connectionInformation.ConnectionId)))
                {
                    _connectedSessions.Add(connectionInformation);
                }
            }
        }

        public void Disconnect(string connectionId)
        {
            lock (_connectedSessionLock)
            {
                var connectionInfo = _connectedSessions.FirstOrDefault(connection => connection.ConnectionId.Equals(connectionId));

                if (connectionInfo != null)
                    _connectedSessions.Remove(connectionInfo);
            }
        }

        #endregion
    }
}
