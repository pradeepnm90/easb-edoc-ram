using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface ICheckListsManager : ISearchableManager<BLL_ChkCategoryTree>
    {
        EntityAction GetEntityActions(BLL_ChkCategoryTree entity);
        EntityResult<IEnumerable<BLL_ChkCategoryTree>> GetAllDealChecklists(int dealnum);
        EntityResult<BLL_ChkCategoryTree> UpdateCheckList(BLL_CheckListParameter bll_checklist);

    }
}

