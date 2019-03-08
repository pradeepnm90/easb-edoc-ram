
using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface IDealDocumentsTransformationManager : IBaseManager
    {
        BLL_KeyDocuments Transform(grs_VKeyDocument dbModel);

        List<BLL_KeyDocuments> Transform(IList<grs_VKeyDocument> data);
    }
}
