using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.WebApi
{
    /// <summary>
    /// This class is a wrapper to the PricingHub. Pricing Hub inherits SignalR.Hub, which requires reference to the
    /// NuGet package. This class does not require that reference.
    /// </summary>
    public class NotificationManager : BaseManager, INotificationManager
    {
        #region Private Variables

        private static IConnectionStore ConnectionStore { get; set; }
        private static INotificationStore NotificationStore { get; set; }

        #endregion Private Variables

        #region Constructors

        public NotificationManager(IUserManager userManager, IConnectionStore connectionStore, INotificationStore notificationStore) : base(userManager)
        {
            ConnectionStore = ValidateObject(connectionStore);
            NotificationStore = ValidateObject(notificationStore);
        }

        #endregion Constructors

        #region Public Methods

        public IReadOnlyList<Notification> GetNotifications(string notificationType = null, LogLevel? logLevel = null, IConvertible notificationId = null, bool broadcast = false)
        {
            string userName = (broadcast) ? null : UserIdentity.UserName;
            return NotificationStore.GetNotifications(userName, notificationType, logLevel, notificationId);
        }

        /// <summary>
        /// Send notification message to active clients
        /// </summary>
        /// <param name="notificationType">Type of notification</param>
        /// <param name="logLevel">Log level (UI display)</param>
        /// <param name="message">Message to display</param>
        /// <param name="data">Additional details (custom class)</param>
        public string Notify(string notificationType, INotificationDetail notificationDetail, LogLevel logLevel, IConvertible notificationId = null, bool broadcast = false)
        {
            string userName = (broadcast) ? null : UserIdentity.UserName;
            Notification notification = NotificationStore.AddUpdate(notificationId.ToString(), notificationType, notificationDetail, userName, logLevel);

            foreach (ConnectionInformation connectionInfo in ConnectionStore.GetAllConnections())
            {
                connectionInfo.Notify(notification);
            }

            return notificationId.ToString();
        }

        public void RemoveNotification(string notificationType, IConvertible notificationId, string userName = null)
        {
            // Validate
            Notification notification = NotificationStore.GetNotification(notificationType, notificationId.ToString(), userName);
            if (notification != null)
            {
                // Remove and update client notification
                NotificationStore.RemoveNotifications(userName, notification.notificationId);
                RemoveNotification(notification);
            }
        }

        public List<Dictionary<string, object>> GetActiveSessions()
        {
            return ConnectionStore.GetAllConnections().Select(session => new Dictionary<string, object>()
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

        public void Connect(string connectionId, string clientAddress, string applicationVersion, string browserVersion)
        {
            ConnectionInformation info = new ConnectionInformation(UserIdentity, connectionId, clientAddress, applicationVersion, browserVersion);
            ConnectionStore.Connect(info);

            // Send updated notification list to all connected clients
            //UpdateNotificationList();
        }

        public void RemoveNotification(string notificationId)
        {
            // Validate
            Notification notification = NotificationStore.GetNotification(notificationId);
            if (notification == null) throw new NotFoundAPIException($"Notification '{notificationId}' was not found in the system!");
            if (string.IsNullOrEmpty(notification.userName)) throw new NotAllowedAPIException("Cannot remove broadcast messages!");
            if (!UserIdentity.UserName.Equals(notification.userName)) throw new NotAllowedAPIException("Cannot remove another users messages!");

            // Remove and update client notifications
            NotificationStore.RemoveNotifications(UserIdentity.UserName, notificationId);
            RemoveNotification(notification);
        }

        public void RemoveNotifications()
        {
            // Remove and update client notifications
            NotificationStore.RemoveNotifications(UserIdentity.UserName);
            UpdateNotificationList();
        }

        public void Disconnect(string connectionId)
        {
            ConnectionStore.Disconnect(connectionId);
        }

        #endregion Internal Hub Methods

        #region Private Methods

        private void UpdateNotificationList()
        {
            IEnumerable<Notification> userNotifications = NotificationStore.GetNotifications(UserIdentity.UserName);
            foreach (ConnectionInformation connectionInfo in ConnectionStore.GetConnections(UserIdentity.UserName))
            {
                connectionInfo.UpdateNotificationList(userNotifications);
            }
        }

        private void RemoveNotification(Notification notification)
        {
            foreach (ConnectionInformation connectionInfo in ConnectionStore.GetConnections(notification.userName))
            {
                connectionInfo.Remove(notification);
            }
        }

        #endregion Private Methods

        public void SendNotifications(string connectionId)
        {
            IEnumerable<Notification> userNotifications = NotificationStore.GetNotifications();
            var connection = ConnectionStore.GetAllConnections().FirstOrDefault(con => con.ConnectionId == connectionId);
            if(connection != null)
                connection.UpdateNotificationList(userNotifications);           
        }
    }
}
