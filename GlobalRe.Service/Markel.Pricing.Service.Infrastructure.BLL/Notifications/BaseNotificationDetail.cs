using System;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    [Serializable]
    public class BaseNotificationDetail : INotificationDetail
    {
        public string message { get; protected set; }

        public BaseNotificationDetail() { }

        public BaseNotificationDetail(string message)
        {
            this.message = message;
        }
    }
}
