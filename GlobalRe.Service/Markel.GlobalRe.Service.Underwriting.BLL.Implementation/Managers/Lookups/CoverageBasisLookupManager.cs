using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups
{
    public class CoverageBasisLookupManager : BaseEntityLookupManager, ICoverageBasisLookupManager
	{
        public CoverageBasisLookupManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ICoverageBasisLookupRepository coverageBasisLookupRepository)
            : base(userManager, cacheStoreManager, coverageBasisLookupRepository) { }
    }
}
