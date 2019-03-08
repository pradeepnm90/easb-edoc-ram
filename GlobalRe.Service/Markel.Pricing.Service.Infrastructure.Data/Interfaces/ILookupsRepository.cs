using Markel.Pricing.Service.Infrastructure.Models;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Data.Interfaces
{
    public interface ILookupsRepository : IBaseRepository
    {
        IList<LookupEntity> GetLookupData();
        IList<LookupEntity> GetLookupDataByConfig(string configSetting);
    }
}
