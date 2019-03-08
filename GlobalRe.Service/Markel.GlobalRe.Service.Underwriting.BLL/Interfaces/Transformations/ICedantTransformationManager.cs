using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface ICedantTransformationManager : IBaseManager
    {
        BLL_Cedant Transform(grs_VGrsCedant dbModel);

        List<BLL_Cedant> Transform(IList<grs_VGrsCedant> data);
    }
}
