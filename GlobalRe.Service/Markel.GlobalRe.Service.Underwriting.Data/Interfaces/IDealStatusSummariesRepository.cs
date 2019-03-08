using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IDealStatusSummariesRepository : IGenericRepository<grs_PrGetGrsDealCountByStatus, int>
    {
        IList<grs_PrGetGrsDealCountByStatus> GetAllDealStatusSummaries(string exposures, string personids);
        IList<grs_VExposureTreeExt> GetGlobalReExposureTree();
        bool ValidPerson(int personId);
    }
}
