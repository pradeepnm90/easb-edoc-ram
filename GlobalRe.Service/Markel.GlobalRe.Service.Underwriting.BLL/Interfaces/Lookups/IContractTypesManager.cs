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

    public interface IContractTypesManager : ISearchableManager<BLL_ContractTypes>
    {
        EntityAction GetEntityActions(BLL_ContractTypes entity);
        EntityResult<IEnumerable<BLL_ContractTypes>> GetContractTypes();

    }
}

