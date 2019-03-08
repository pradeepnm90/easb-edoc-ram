using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups
{
    public interface IDealStatusesLookupManager : ILookupsManager
    {
        IList<int> GetGlobalReDealStatusCodes();
        IList<string> GetGlobalReDealStatusNames();
        IEnumerable<LookupEntity> GetByConfig(string groupName);

    }
}
