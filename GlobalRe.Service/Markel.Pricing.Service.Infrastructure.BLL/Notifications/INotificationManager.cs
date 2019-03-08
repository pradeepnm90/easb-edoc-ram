using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public interface INotificationManager : IBaseManager
    {
        void Connect(string connectionId, string clientAddress, string applicationVersion, string browserVersion);
        void Disconnect(string connectionId);
        void RemoveNotification(string notificationId);
        void RemoveNotifications();

        IReadOnlyList<Notification> GetNotifications(string notificationType = null, LogLevel? logLevel = null, IConvertible notificationId = null, bool broadcast = false);
        string Notify(string notificationType, INotificationDetail notificationDetail, LogLevel logLevel, IConvertible notificationId = null, bool broadcast = false);
        void RemoveNotification(string notificationType, IConvertible notificationId, string userName = null);
        List<Dictionary<string, object>> GetActiveSessions();
        void SendNotifications(string connectionId);
    }
}
