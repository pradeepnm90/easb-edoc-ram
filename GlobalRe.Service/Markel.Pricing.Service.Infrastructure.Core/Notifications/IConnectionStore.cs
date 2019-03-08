using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public interface IConnectionStore
    {
        List<ConnectionInformation> GetAllConnections();
        List<ConnectionInformation> GetConnections(string userName);
        void Connect(ConnectionInformation connectionInformation);
        void Disconnect(string connectionId);
    }
}
