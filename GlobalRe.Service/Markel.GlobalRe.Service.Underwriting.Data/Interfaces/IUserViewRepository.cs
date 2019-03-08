using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Data;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IUserViewRepository : ISearchRepository<grs_TblUserview, int>
    {
        //void ResetDefaultOnUserViews(int userId, string screenName, int viewId);
        int GetNextSortOrder(int userId, string screenname);
    }
}
