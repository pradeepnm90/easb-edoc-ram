using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IDealManager : ISearchableManager<BLL_Deal>
    {
        EntityAction GetEntityActions(BLL_Deal bLL_Deal);
        //IList<int> GetStatusCodes();

        EntityResult<BLL_Deal> UpdateDeal(BLL_Deal bLL_Deal);
		EntityResult<IEnumerable<BLL_Deal>> GetWorkbenchDeals(SearchCriteria criteria);
		EntityResult<BLL_Deal> GetDeal(int dealNumber);
		bool Lock(int dealNumber);
		bool Unlock(int dealNumber);
		EntityResult<BLL_EntityLock> GetLocks(int dealNumber);


	}
}
