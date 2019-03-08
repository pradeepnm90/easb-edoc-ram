using System;
using System.Collections.Generic;
using System.Linq;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups
{
    public class DealStatusesLookupManager : BaseEntityLookupManager, IDealStatusesLookupManager
    {
        private const string DEAD_DECLINE_SETTING = "GlobalRe Deal Decline Statuses";
        private const string DEAD_DECLINE_GROUP = "decline";
        private static object CacheLock = new object();

        public DealStatusesLookupManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, IDealStatusesLookupRepository dealStatusesLookupRepository)
            : base(userManager, cacheStoreManager, dealStatusesLookupRepository) { }

        public IList<int> GetGlobalReDealStatusCodes()
        {
            return GetLookupData().Select(a => Convert.ToInt32(a.Code)).ToList();
        }

        public IList<string> GetGlobalReDealStatusNames()
        {
            return GetLookupData().Select(a => a.Description.ToLower()).ToList();
        }

        
        private string GetConfigSetting(string groupName)
        {
            if (groupName.ToLower() == DEAD_DECLINE_GROUP)
                return DEAD_DECLINE_SETTING;
            return string.Empty;
        }

        public IEnumerable<LookupEntity> GetByConfig(string groupName)
        {
            return RunInContextScope(() =>
            {
                lock (CacheLock)
                {
                    string configSetting = GetConfigSetting(groupName);
                    string cacheStoreName = UserIdentity.EnvironmentName;
                    string cacheKey = CacheManager.BuildKey(configSetting, this.GetType().Name.Replace("Manager", ""));
                    IList<LookupEntity> cachedConfigData = CacheManager.GetItem<IList<LookupEntity>>(cacheStoreName, cacheKey, (action) =>
                    {
                        IList<LookupEntity> configData = LookupsRepository.GetLookupDataByConfig(configSetting);
                        return new CacheItem(configData);
                    }, false);
                    return cachedConfigData;
                }
            }, true);
        }
    }
}
