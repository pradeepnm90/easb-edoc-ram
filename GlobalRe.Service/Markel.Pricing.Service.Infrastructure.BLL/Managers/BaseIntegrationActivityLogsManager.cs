using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseIntegrationActivityLogsManager : BaseManager, IIntegrationActivityLogsManager
    {
        #region Constructor

        public BaseIntegrationActivityLogsManager(IUserManager userManager) : base(userManager) { }

        #endregion Constructor

        #region Methods

        public string Log<T>(string source, T? referenceKey, string messageType, object priorEntity, object request, string onBehalfOf = null) where T : struct
        {
            if (!referenceKey.HasValue) throw new IllegalArgumentAPIException("referenceKey", "Missing required field!");
            return Log(source, referenceKey.ToString(), messageType, priorEntity, request, onBehalfOf);
        }

        public abstract string Log(string source, string referenceKey, string messageType, object priorEntity, object request, string onBehalfOf = null);

        public abstract void Update(string integrationLogKey, string status, string message = null, object modifiedEntity = null);

        #endregion Methods
    }
}
