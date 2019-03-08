using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
	public interface IPersonsManager : ISearchableManager<BLL_Person>
	{
		EntityAction GetEntityActions(BLL_Person bLL_Person);
		EntityResult<BLL_Person> GetPerson(int personId);
	}
}
