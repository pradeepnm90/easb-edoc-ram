using Markel.Pricing.Service.Infrastructure.Constants;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    public interface INotificationManager
    {
        string Notify(string notificationType, string notificationTypeDescription, NotificationLogLevel logLevel, string message, object data = null, string notificationId = null);
        List<Dictionary<string, object>> GetActiveSessions();
    }
}
