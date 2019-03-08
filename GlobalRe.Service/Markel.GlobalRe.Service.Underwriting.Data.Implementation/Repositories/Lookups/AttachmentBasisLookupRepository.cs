using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Repositories.Lookups;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
	public class AttachmentBasisLookupRepository : CatalogItemsExtRepository, IAttachmentBasisLookupRepository
	{
		//[ExcludeFromCodeCoverage]
		public AttachmentBasisLookupRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }
        protected override string CatalogItemName { get { return "Policy Basis"; } }
	}

}
