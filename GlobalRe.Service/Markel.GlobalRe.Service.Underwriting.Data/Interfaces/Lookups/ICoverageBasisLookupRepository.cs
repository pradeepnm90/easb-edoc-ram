using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups
{
    public interface ICoverageBasisLookupRepository : IGenericRepository<grs_VCatalogItemsExt, int>, ILookupsRepository { }
}
