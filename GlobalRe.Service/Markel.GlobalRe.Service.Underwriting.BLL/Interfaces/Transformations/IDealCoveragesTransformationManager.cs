using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface IDealCoveragesTransformationManager : IBaseManager
    {
        BLL_DealCoverages Transform(grs_VGrsDealCoverage dbModel);

        List<BLL_DealCoverages> Transform(IList<grs_VGrsDealCoverage> data);
    }
}
