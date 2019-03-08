using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseEntityLookupManager : BaseLookupManager
    {
        #region Properties

        protected ICacheStoreManager CacheManager { get; private set; }
        protected ILookupsRepository LookupsRepository { get; private set; }

        #endregion Properties

        #region Constructors

        public BaseEntityLookupManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILookupsRepository lookupsRepository) : base(userManager)
        {
            CacheManager = ValidateObject(cacheStoreManager);
            LookupsRepository = ValidateRepository(lookupsRepository);
        }

        #endregion

        #region Methods

        // Local (Lifetime of the API call) reference to Lookup Data.
        private IList<LookupEntity> LookupData { get; set; }

        private static object CacheLock = new object();
        protected internal override IList<LookupEntity> GetLookupData()
        {
            lock (CacheLock)
            {
                // Use Local reference if it's set to save time from going to cache manager
                if (LookupData != null) return LookupData;

                string cacheKey = CacheManager.BuildKey("Lookup", this.GetType().Name.Replace("Manager", ""));
                IList<LookupEntity> cachedLookupData = CacheManager.GetItem<IList<LookupEntity>>(cacheKey, (action) =>
                {
                    IList<LookupEntity> lookupData = RunInContextScope(() => LookupsRepository.GetLookupData(), true);
                    return new CacheItem(lookupData);
                }, false);

                return LookupData = cachedLookupData;
            }
        }

        #endregion Methods
    }
}
