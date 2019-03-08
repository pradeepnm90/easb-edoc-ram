using Markel.Pricing.Service.Infrastructure.WebApi;
using Microsoft.AspNet.SignalR;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public class MarkelHub : Hub
    {
        #region Connection

        public override System.Threading.Tasks.Task OnConnected()
        {
            Connect();

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            NotificationManager.Disconnect(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            Connect();

            return base.OnReconnected();
        }

        #endregion Connection

        #region Custom Messages

        public void RemoveNotification(string notificationId)
        {
            string connectionId = Context.ConnectionId;

            NotificationManager.RemoveNotification(connectionId, notificationId);
        }

        #endregion Custom Messages

        #region Private

        private void Connect()
        {
            string connectionId = Context.ConnectionId;
            string userName = Context.QueryString["userName"];
            string domainName = Context.QueryString["domainName"];
            string environmentName = Context.QueryString["environmentName"];

            string applicationVersion = Context.QueryString["applicationVersion"];
            string browserVersion = Context.QueryString["browserVersion"];
            string clientAddress = (Context.Request.Environment["server.RemoteIpAddress"] ?? "N/A").ToString();

            NotificationManager.Connect(connectionId, userName, domainName, environmentName, clientAddress, applicationVersion, browserVersion);
        }

        #endregion Private
    }
}