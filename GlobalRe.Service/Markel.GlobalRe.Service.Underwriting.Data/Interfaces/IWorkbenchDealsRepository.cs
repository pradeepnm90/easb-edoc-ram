using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IWorkbenchDealsRepository : ISearchRepository<grs_VGrsDealsByStatu, int>
    {
        IList<int> GetStatusCodes();
        IList<grs_VGrsDealsByStatu> GetDeals(SearchCriteria searchCriteria);
        IList<grs_VExposureTreeExt> GetGlobalReExposureTree();
        bool ValidPerson(int personId);
    }
   
}
