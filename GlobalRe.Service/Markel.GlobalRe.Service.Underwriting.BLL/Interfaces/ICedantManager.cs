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
    public interface ICedantManager : ISearchableManager<BLL_Cedant>
    {
        EntityAction GetEntityActions(BLL_Cedant bLL_Cedant);

        //EntityResult<IEnumerable<BLL_Cedants>> GetCedants();

        EntityResult<IEnumerable<BLL_Cedant>> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid);
		bool CedantHasReinsuranceCedantGroup(int? cedant);
	}
}
