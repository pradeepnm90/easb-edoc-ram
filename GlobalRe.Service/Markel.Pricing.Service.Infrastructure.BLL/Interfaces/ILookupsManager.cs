using Markel.Pricing.Service.Infrastructure.Models;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    public interface ILookupsManager : IBaseManager
    {
        IList<LookupEntity> GetAll();
        IEnumerable<LookupEntity> GetByGroupName(string groupName);
        LookupEntity GetByID(int? id);
        LookupEntity GetByCode(string code);
        LookupEntity GetByCode(string group, string code);
        int? GetIDByCode(string code);
        int? GetIDByCode(string group, string code);
        int? GetIDByDescription(string group, string description);
    }
}
