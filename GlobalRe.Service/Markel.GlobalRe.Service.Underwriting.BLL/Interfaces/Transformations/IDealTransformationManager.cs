using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface IDealTransformationManager : IBaseManager
    {
        BLL_Deal Transform(grs_VGrsDeal dbModel);
        BLL_Deal Transform(grs_VGrsDealsByStatu dbModel);
        List<BLL_ExposureTree> Transform(IList<grs_VExposureTreeExt> exposureTree);
    }
}
