using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class NoteTypesLookupManager : BaseEntityLookupManager, INoteTypesLookupManager
	{

		public NoteTypesLookupManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, INoteTypesLookupRepository noteTypesLookupRepository)
		: base(userManager, cacheStoreManager, noteTypesLookupRepository) { }
	}
}

