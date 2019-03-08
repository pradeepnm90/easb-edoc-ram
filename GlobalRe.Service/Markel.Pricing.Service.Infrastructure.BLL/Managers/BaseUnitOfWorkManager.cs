using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseUnitOfWorkManager : BaseBusinessManager
    {
        #region Constants

        private const string UNIT_OF_WORK_LOCK = "UnitOfWorkLock";

        // Cache Lock Timeout for a given Unit of Work
        private static TimeSpan UNIT_OF_WORK_TIMEOUT = MarkelConfiguration.UnitOfWorkTimeout;

        #endregion Constants

        #region Constructors

        public BaseUnitOfWorkManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager)
            : base(userManager, cacheStoreManager, logManager) { }

        #endregion

        #region Unit of Work

        protected void UnitOfWorkLock(Enum entityType, IConvertible entityId)
        {
            // No Lock if current process is a Background Process
            if (IsBackgroundProcess) return;

            Guid lockKey = Guid.NewGuid();
            string cacheKey = $"{entityType}_{entityId}.{UNIT_OF_WORK_LOCK}";

            // Lock Request
            Guid cachedLockKey = CacheManager.GetItem<Guid>(cacheKey, (action) =>
            {
                return new CacheItem(UNIT_OF_WORK_TIMEOUT, UserIdentity.UserName, lockKey);
            });

            // Check Lock
            if (!lockKey.Equals(cachedLockKey)) throw new TooManyRequestsAPIException("Too Many Requests!");
        }

        protected void UnitOfWorkUnLock(Enum entityType, IConvertible entityId)
        {
            // No Unlock if current process is a Background Process
            if (IsBackgroundProcess) return;

            string cacheKey = $"{entityType}_{entityId}.{UNIT_OF_WORK_LOCK}";

            // Unlock Request
            CacheManager.Remove<Guid>(cacheKey);
        }

        #endregion Unit of Work
    }
}
