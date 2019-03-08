using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Data;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IUserViewScreenRepository : ISearchRepository<grs_VGrsUserView, int>
    {

    }
}

