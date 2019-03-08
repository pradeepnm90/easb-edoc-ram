using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Repositories.Lookups;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
	//[ExcludeFromCodeCoverage]
	public class NoteTypesLookupRepository : CatalogItemsExtRepository, INoteTypesLookupRepository
	{
		public NoteTypesLookupRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        protected override string CatalogItemName { get { return "Note Type"; } }
	}
}

