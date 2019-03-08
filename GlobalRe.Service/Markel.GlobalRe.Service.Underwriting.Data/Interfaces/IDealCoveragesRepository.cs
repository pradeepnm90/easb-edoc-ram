using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IDealCoveragesRepository : ISearchRepository<grs_VGrsDealCoverage, int>
    {
        IList<grs_VGrsDealCoverage> GetDealCoverages(int dealNumber);
    }

}
