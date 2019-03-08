using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Lookups
{
    [ExcludeFromCodeCoverage]
    public class RolePersonsManager : BaseEntityLookupManager, IRolePersonsManager
    {
        public RolePersonsManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, IRolePersonsLookupRepository rolePersonsLookupRepository) : base(userManager, cacheStoreManager, rolePersonsLookupRepository) { }
    }
}
