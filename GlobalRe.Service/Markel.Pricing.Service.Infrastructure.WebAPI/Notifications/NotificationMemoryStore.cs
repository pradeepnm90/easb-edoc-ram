using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    internal class NotificationMemoryStore : INotificationStore
    {
        #region Private Variables

        private static object _notificationLock = new object();
        private static List<Notification> _notifications = new List<Notification>();

        #endregion Private Variables

        #region Constructor

        public NotificationMemoryStore(IUserManager userManager) { }

        #endregion Constructor

        #region Public Methods

        public IReadOnlyList<Notification> GetNotifications(string userName = null, string notificationType = null, LogLevel? logLevel = null, IConvertible notificationId = null)
        {
            lock (_notificationLock)
            {
                return GetNotifications(_notifications, userName, notificationType, logLevel?.ToString(), notificationId?.ToString());
            }
        }

        public Notification GetNotification(string notificationId)
        {
            lock (_notificationLock)
            {
                return _notifications.FirstOrDefault(n => n.notificationId.Equals(notificationId));
            }
        }

        public Notification GetNotification(string notificationType, string notificationId, string userName)
        {
            lock (_notificationLock)
            {
                return _notifications.FirstOrDefault(n =>
                    n.notificationType.Equals(notificationType) &&
                    n.notificationId.Equals(notificationId) &&
                    n.userName == userName
                );
            }
        }

        public Notification AddUpdate(string notificationId, string notificationType, INotificationDetail notificationDetail, string userName, LogLevel logLevel)
        {
            Notification notification = new Notification(notificationId, notificationType, notificationDetail, userName, logLevel);

            lock (_notificationLock)
            {
                IReadOnlyList<Notification> notifications = GetNotifications(_notifications, userName);

                Notification existingNotification = notifications.FirstOrDefault(n => n.notificationId.Equals(notification.notificationId));
                if (existingNotification != null)
                {
                    // Remove Existing Notification
                    _notifications.Remove(existingNotification);
                }

                _notifications.Add(notification);
            }
            
            return notification;
        }

        public void RemoveNotifications(string userName, string notificationId = null)
        {
            lock (_notificationLock)
            {
                IReadOnlyList<Notification> notifications = GetNotifications(_notifications, userName);
                if (string.IsNullOrEmpty(notificationId))
                {
                    // Remove All Notifications
                    foreach (Notification notification in notifications)
                    {
                        _notifications.Remove(notification);
                    }
                }
                else
                {
                    Notification userNotification = notifications.FirstOrDefault(n => n.notificationId.Equals(notificationId));
                    if (userNotification != null)
                    {
                        _notifications.Remove(userNotification);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private static IReadOnlyList<Notification> GetNotifications(List<Notification> notifications, string userName, string notificationType = null, string logLevel = null, string notificationId = null)
        {
            //IEnumerable<Notification> filtered = notifications.Where(n => n.userName == null || n.userName == userName);
            IEnumerable<Notification> filtered = notifications;

            if (notificationType.HasValue())
                filtered = filtered.Where(n => n.notificationType.Equals(notificationType));

            if (logLevel.HasValue())
                filtered = filtered.Where(n => n.logLevel.Equals(logLevel));

            if (notificationId.HasValue())
                filtered = filtered.Where(n => n.notificationId.Equals(notificationId));

            return filtered.ToList().AsReadOnly();
        }

        #endregion Private Methods
    }
}
