using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups
{
    public interface IExposureTreeManager : IGlobalReBusinessManager<BLL_ExposureTree>
    {
        EntityResult<IEnumerable<BLL_ExposureTree>> GetGlobalReExposureTree();
    }
}
