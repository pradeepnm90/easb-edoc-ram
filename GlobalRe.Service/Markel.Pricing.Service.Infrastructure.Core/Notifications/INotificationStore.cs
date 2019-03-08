using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public interface INotificationStore
    {
        IReadOnlyList<Notification> GetNotifications(string userName = null, string notificationType = null, LogLevel? logLevel = null, IConvertible notificationId = null);
        Notification GetNotification(string notificationId);
        Notification GetNotification(string notificationType, string notificationId, string userName);
        Notification AddUpdate(string notificationId, string notificationType, INotificationDetail notificationDetail, string userName, LogLevel logLevel);
        void RemoveNotifications(string userName, string notificationId = null);
    }
}
