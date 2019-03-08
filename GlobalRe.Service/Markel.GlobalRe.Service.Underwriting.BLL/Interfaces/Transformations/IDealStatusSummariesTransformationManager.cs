using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
	public interface IDealStatusSummariesTransformationManager : IBaseManager
	{
		List<BLL_DealStatusSummary> Transform(IList<grs_PrGetGrsDealCountByStatus> dealStatusSummary);
        List<BLL_ExposureTree> Transform(IList<grs_VExposureTreeExt> exposureTree);        
    }
}
