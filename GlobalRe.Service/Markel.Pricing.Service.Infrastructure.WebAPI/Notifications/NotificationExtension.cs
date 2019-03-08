using Markel.Pricing.Service.Infrastructure.Helpers;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    internal static class NotificationExtension
    {
        private static string ServerVersion = IOHelper.GetServerVersion();

        public static void UpdateNotificationList(this ConnectionInformation connectionInfo, IEnumerable<Notification> userNotifications)
        {
            connectionInfo.Client().UpdateNotificationList(userNotifications, ServerVersion);
        }

        public static void Notify(this ConnectionInformation connectionInfo, Notification notification)
        {
            connectionInfo.Client().Notify(notification);
        }

        public static void Remove(this ConnectionInformation connectionInfo, Notification notification)
        {
            connectionInfo.Client().Remove(notification);
        }

        private static dynamic Client(this ConnectionInformation connectionInfo)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MarkelHub>();
            return context.Clients.Client(connectionInfo.ConnectionId);
        }
    }
}
