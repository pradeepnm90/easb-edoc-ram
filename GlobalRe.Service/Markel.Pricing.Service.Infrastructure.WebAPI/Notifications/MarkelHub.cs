using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Providers;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public class MarkelHub : Hub
    {
        #region Properties

        private IUserManager UserManager { get; set; }
        private INotificationManager NotificationManager { get; set; }

        #endregion Properties

        #region Constructor

        public MarkelHub(IUserManager userManager, INotificationManager notificationManager)
        {
            UserManager = userManager;
            NotificationManager = notificationManager;
        }

        #endregion Constructor

        #region Connection

        public override Task OnConnected()
        {
            Connect();

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            NotificationManager.Disconnect(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Connect();

            return base.OnReconnected();
        }

        #endregion Connection

        #region Custom Messages

        public void RemoveNotification(string notificationId)
        {
            if (AuthorizeConnection())
            {
                NotificationManager.RemoveNotification(notificationId);
            }
        }

        public void RemoveNotifications()
        {
            if (AuthorizeConnection())
            {
                NotificationManager.RemoveNotifications();
            }
        }

        public void UpdateNotifications()
        {
            string connectionId = Context.ConnectionId;
            NotificationManager.SendNotifications(connectionId);
        }

        #endregion Custom Messages

        #region Private

        private bool AuthorizeConnection()
        {
            string token = Context.QueryString["token"];
            var identity = AuthProvider.BearerOptions.AccessTokenFormat.Unprotect(token)?.Identity;
            if (identity?.IsAuthenticated == true)
            {
                string userName = identity.GetClaimValue("userName");
                string domainName = identity.GetClaimValue("domainName");

                try
                {
                    UserManager.Initialize($"{userName}@{domainName}");
                    return true;
                }
                catch (Exception)
                {
                    // TODO: Log Exception
                }
            }

            return false;
        }

        private void Connect()
        {
            if (AuthorizeConnection())
            {
                string connectionId = Context.ConnectionId;
                string clientAddress = (Context.Request.Environment["server.RemoteIpAddress"] ?? "N/A").ToString();
                string applicationVersion = Context.QueryString["applicationVersion"];
                string browserVersion = Context.QueryString["browserVersion"];

                NotificationManager.Connect(connectionId, clientAddress, applicationVersion, browserVersion);
            }
        }

        #endregion Private
    }
}