using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IDealStatusSummariesManager : IGlobalReBusinessManager<BLL_DealStatusSummary>
    {
        EntityAction GetEntityActions(BLL_DealStatusSummary bLL_DealStatusSummary);

        EntityResult<IEnumerable<BLL_DealStatusSummary>> GetAllDealStatusSummaries(SearchCriteria criteria);
    }
}
