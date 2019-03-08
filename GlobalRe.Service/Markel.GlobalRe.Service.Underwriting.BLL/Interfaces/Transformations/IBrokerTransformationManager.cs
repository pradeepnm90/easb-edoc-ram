using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface IBrokerTransformationManager : IBaseManager
    {
        BLL_Broker Transform(grs_VGrsBroker dbModel);

        List<BLL_Broker> Transform(IList<grs_VGrsBroker> data);
    }
}
