using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IExposureTreeRepository : IGenericRepository<grs_VExposureTreeExt, int>
    {
        IList<grs_VExposureTreeExt> GetGlobalReExposureTree();
    }

}


