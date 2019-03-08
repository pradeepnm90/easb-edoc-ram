using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface ICedantRepository : ISearchRepository<grs_VGrsCedant, int>
    {
		IList<grs_VGrsCedant> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid);
		//IList<grs_VGrsWritingCompany> GetWritingCompany(Boolean isGRSFlag);
	}
}
