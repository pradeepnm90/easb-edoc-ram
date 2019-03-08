using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Repositories.Lookups
{
	//[ExcludeFromCodeCoverage]
	public class CoverageBasisLookupRepository : CatalogItemsExtRepository, ICoverageBasisLookupRepository
	{
        public CoverageBasisLookupRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        protected override string CatalogItemName { get { return "Cover Type"; } }
    }
}
