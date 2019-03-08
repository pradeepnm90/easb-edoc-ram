using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface ICheckListsTransformationManager : IBaseManager
    {
        List<BLL_ChkCategoryTree> Transform(IList<grs_VGrsChecklistsByDeal> dealChklist);
        BLL_ChkCategoryTree Transform(IList<grs_VGrsChecklistsByDeal> dealChklist, int count);

    }
}
