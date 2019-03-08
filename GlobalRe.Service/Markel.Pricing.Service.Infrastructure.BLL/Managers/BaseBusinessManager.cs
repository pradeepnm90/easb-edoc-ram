using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseBusinessManager : BaseManager
    {
        #region Properties

        protected ICacheStoreManager CacheManager { get; private set; }
        protected ILogManager LogManager { get; private set; }

        #endregion Properties

        #region Constructors

        public BaseBusinessManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager) : base(userManager)
        {
            CacheManager = ValidateObject(cacheStoreManager);
            LogManager = ValidateObject(logManager);
        }

        #endregion
    }
}
