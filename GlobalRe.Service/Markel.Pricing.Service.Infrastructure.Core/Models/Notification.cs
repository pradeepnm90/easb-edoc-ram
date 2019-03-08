using Markel.Pricing.Service.Infrastructure.Constants;
using Markel.Pricing.Service.Infrastructure.Extensions;
using System;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class Notification
    {
        public string NotificationId { get; private set; }
        public string NotificationType { get; private set; }
        public string NotificationDescription { get; private set; }
        public string LogLevel { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public DateTime NotificationDate { get; private set; }

        public Notification(string notificationId, string notificationType, string notificationTypeDescription, NotificationLogLevel logLevel, string message, object data)
        {
            NotificationId = notificationId;
            NotificationType = notificationType;
            NotificationDescription = notificationTypeDescription;
            LogLevel = logLevel.ToString();
            Message = message;
            Data = data;

            NotificationDate = DateTime.Now;
        }
    }
}
