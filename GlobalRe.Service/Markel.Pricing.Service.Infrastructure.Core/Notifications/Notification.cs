using Markel.Pricing.Service.Infrastructure.Logging;
using System;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public class Notification
    {
        #region Properties

        public string notificationId { get; private set; }
        public string notificationType { get; private set; }
        public INotificationDetail detail { get; private set; }
        public string logLevel { get; private set; }
        public DateTime notificationDate { get; private set; }
        public string userName { get; private set; }

        #endregion Properties

        #region Constructors

        public Notification(string notificationType, INotificationDetail detail, string userName, LogLevel logLevel)
        {
            this.notificationType = notificationType;
            this.detail = detail;
            this.userName = userName;
            this.logLevel = logLevel.ToString();

            notificationDate = DateTime.Now;
        }

        public Notification(string notificationId, string notificationType, INotificationDetail detail, string userName, LogLevel logLevel)
            : this(notificationType, detail, userName, logLevel)
        {
            this.notificationId = string.IsNullOrEmpty(notificationId) ? Guid.NewGuid().ToString() : notificationId;
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            return $"[{logLevel}] {notificationType}[{notificationId}]: {detail.message} [{userName ?? "Broadcast"}]";
        }

        #endregion Methods
    }
}
