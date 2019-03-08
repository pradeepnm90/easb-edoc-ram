using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IWritingCompanyManager : ISearchableManager<BLL_WritingCompany>
    {
        EntityAction GetEntityActions(BLL_WritingCompany bLL_WritingCompany);

        EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany();

        EntityResult<IEnumerable<BLL_WritingCompany>> GetWritingCompany(Boolean isGRSFlag);
    }
}
