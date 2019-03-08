using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface IWritingCompanyTransformationManager : IBaseManager
    {
        BLL_WritingCompany Transform(grs_VPaperExt dbModel);

        List<BLL_WritingCompany> Transform(IList<grs_VPaperExt> data);
    }
}
