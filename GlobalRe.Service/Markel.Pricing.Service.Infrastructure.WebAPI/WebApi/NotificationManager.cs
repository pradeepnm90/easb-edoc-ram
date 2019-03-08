using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.WebApi
{
    /// <summary>
    /// This class is a wrapper to the PricingHub. Pricing Hub inherits SignalR.Hub, which requires reference to the
    /// NuGet package. This class does not require that reference.
    /// </summary>
    public class NotificationManager : INotificationManager
    {
        #region Private Classes

        private class ConnectionInformation
        {
            public string UserName { get; private set; }
            public string DomainName { get; private set; }
            public string EnvironmentName { get; private set; }
            public string ConnectionId { get; private set; }
            public string ClientAddress { get; private set; }
            public string ApplicationVersion { get; private set; }
            public string BrowserVersion { get; private set; }
            public DateTime ConnectedDate { get; private set; }

            public ConnectionInformation(string userName, string domainName, string environmentName, string connectionId, string clientAddress, string applicationVersion, string browserVersion)
            {
                UserName = userName;
                DomainName = domainName;
                EnvironmentName = environmentName;
                ConnectionId = connectionId;

                ClientAddress = clientAddress;
                ApplicationVersion = applicationVersion;
                BrowserVersion = browserVersion;

                ConnectedDate = DateTime.Now;
            }

            public bool Equals(string userName, string domainName, string environmentName)
            {
                return (UserName != null) && UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) &&
                    (DomainName != null) && DomainName.Equals(domainName, StringComparison.InvariantCultureIgnoreCase) &&
                    (EnvironmentName != null) && EnvironmentName.Equals(environmentName, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion Private Classes

        #region Private Variables

        private static object _notificationLock = new object();
        private static object _connectedSessionLock = new object();

        private static List<ConnectionInformation> _connectedSessions = new List<ConnectionInformation>();
        private static Dictionary<string, List<Notification>> _notifications = new Dictionary<string, List<Notification>>();

        private static string _serverVersion = IOHelper.GetServerVersion();

        private UserIdentity UserIdentity;

        #endregion Private Variables

        #region Constructors

        public NotificationManager(IUserManager userManager)
        {
            UserIdentity = userManager.UserIdentity;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Send notification message to active clients
        /// </summary>
        /// <param name="notificationType">Type of notification</param>
        /// <param name="logLevel">Log level (UI display)</param>
        /// <param name="message">Message to display</param>
        /// <param name="data">Additional details (custom class)</param>
        public string Notify(string notificationType, string notificationTypeDescription, NotificationLogLevel logLevel, string message, object data = null, string notificationId = null)
        {
            bool isUpdate = !string.IsNullOrEmpty(notificationId);
            notificationId = isUpdate ? notificationId : Guid.NewGuid().ToString();

            string userName = this.UserIdentity.UserName;
            string domainName = this.UserIdentity.DomainName;
            string environmentName = this.UserIdentity.EnvironmentName;

            Notification notification = new Notification(notificationId, notificationTypeDescription, notificationType, logLevel, message, data);
            lock (_notificationLock)
            {
                string notificationTag = string.Format("{0}|{1}|{2}", userName, domainName, environmentName);
                if (!_notifications.ContainsKey(notificationTag))
                {
                    _notifications.Add(notificationTag, new List<Notification>());
                }

                var notifications = _notifications[notificationTag];

                if (isUpdate)
                {
                    // Remove Existing Notification
                    var existingNotification = notifications.FirstOrDefault(n => n.NotificationId.Equals(notificationId));
                    notifications.Remove(existingNotification);
                }

                notifications.Add(notification);
            }

            foreach (var connectionInfo in _connectedSessions.Where(connection => connection.Equals(userName, domainName, environmentName)))
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<MarkelHub>();
                context.Clients.Client(connectionInfo.ConnectionId).Notify(notification);
            }

            return notificationId;
        }

        public List<Dictionary<string, object>> GetActiveSessions()
        {
            return _connectedSessions.Select(session => new Dictionary<string, object>()
            {
                { "UserName", session.UserName },
                { "DomainName", session.DomainName },
                { "EnvironmentName", session.EnvironmentName },
                { "ClientAddress", session.ClientAddress },
                //{ "ClientHostName", ServiceMetrics.GetHostName(session.ClientAddress) },
                { "ApplicationVersion", session.ApplicationVersion },
                { "BrowserVersion", session.BrowserVersion },
                { "ConnectedDate", session.ConnectedDate }
            }).ToList();
        }

        #endregion Public Methods

        #region Internal Hub Methods

        internal static void Connect(string connectionId, string userName, string domainName, string environmentName, string clientAddress, string applicationVersion, string browserVersion)
        {
            lock (_connectedSessionLock)
            {
                if (!_connectedSessions.Exists(connection => connection.ConnectionId.Equals(connectionId)) &&
                    !string.IsNullOrEmpty(userName) &&
                    !string.IsNullOrEmpty(domainName) &&
                    !string.IsNullOrEmpty(environmentName))
                {
                    _connectedSessions.Add(new ConnectionInformation(userName, domainName, environmentName, connectionId, clientAddress, applicationVersion, browserVersion));
                }

                // Send updated notification list to all connected clients
                var userNotifications = GetNotifications(userName, domainName, environmentName);
                foreach (var connectionInfo in _connectedSessions.Where(connection => connection.Equals(userName, domainName, environmentName)))
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<MarkelHub>();
                    context.Clients.Client(connectionInfo.ConnectionId).UpdateNotificationList(userNotifications, _serverVersion);
                }
            }
        }

        internal static void RemoveNotification(string connectionId, string notificationId)
        {
            ConnectionInformation connectedSession = null;

            lock (_connectedSessionLock)
            {
                connectedSession = _connectedSessions.FirstOrDefault(session => session.ConnectionId.Equals(connectionId));
            }

            if (connectedSession != null)
            {
                lock (_notificationLock)
                {
                    string key = string.Format("{0}|{1}|{2}", connectedSession.UserName, connectedSession.DomainName, connectedSession.EnvironmentName);
                    if (_notifications.ContainsKey(key))
                    {
                        var userNotifications = _notifications[key];
                        if (string.IsNullOrEmpty(notificationId))
                        {
                            // Remove All Notifications
                            userNotifications.Clear();
                        }
                        else
                        {
                            var userNotification = userNotifications.FirstOrDefault(notification => notification.NotificationId.Equals(notificationId));
                            if (userNotification != null)
                            {
                                userNotifications.Remove(userNotification);
                            }
                        }
                    }
                }

                UpdateNotificationList(connectedSession.UserName, connectedSession.DomainName, connectedSession.EnvironmentName);
            }
        }

        internal static void Disconnect(string connectionId)
        {
            lock (_connectedSessionLock)
            {
                var connectionInfo = _connectedSessions.FirstOrDefault(connection => connection.ConnectionId.Equals(connectionId));

                if (connectionInfo != null)
                    _connectedSessions.Remove(connectionInfo);
            }
        }

        #endregion Internal Hub Methods

        #region Private Methods

        private static List<Notification> GetNotifications(string userName, string domainName, string environmentName)
        {
            lock (_notificationLock)
            {
                string key = string.Format("{0}|{1}|{2}", userName, domainName, environmentName);
                if (_notifications.ContainsKey(key))
                {
                    return _notifications[key];
                }
                else
                {
                    return default(List<Notification>);
                }
            }
        }

        private static void UpdateNotificationList(string userName, string domainName, string environmentName)
        {
            var userNotifications = GetNotifications(userName, domainName, environmentName);
            foreach (var connectionInfo in _connectedSessions.Where(connection => connection.Equals(userName, domainName, environmentName)))
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<MarkelHub>();
                context.Clients.Client(connectionInfo.ConnectionId).UpdateNotificationList(userNotifications, _serverVersion);
            }
        }

        #endregion Private Methods
    }
}
