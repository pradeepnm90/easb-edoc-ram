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
    public interface IDealCoveragesManager : ISearchableManager<BLL_DealCoverages>
    {
        EntityAction GetEntityActions(BLL_DealCoverages bLL_DealCoverages);

        EntityResult<IEnumerable<BLL_DealCoverages>> GetDealCoverages(int dealNumber);
    }
}
