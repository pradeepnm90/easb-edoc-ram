using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class AttachmentBasisLookupManager : BaseEntityLookupManager, IAttachmentBasisLookupManager
	{
		public AttachmentBasisLookupManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, IAttachmentBasisLookupRepository attachmentBasisLookupRepository)
			: base(userManager, cacheStoreManager, attachmentBasisLookupRepository) { }
	}

}
