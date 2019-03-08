using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface INotesTransformationManager : IBaseManager
    {
        List<BLL_Notes> Transform(IList<grs_VGrsNote> dealStatusSummary);
        //BLL_Notes Transform(TbDealnote dbModel);
        BLL_Notes Transform(grs_VGrsNote dbModel);

    }
}
