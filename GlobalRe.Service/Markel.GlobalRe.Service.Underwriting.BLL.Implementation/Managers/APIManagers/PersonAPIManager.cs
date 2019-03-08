using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class PersonAPIManager : BaseUnitOfWorkManager, IPersonAPIManager
    {

        #region Repositories & Managers
        private IPersonsManager _personManager { get; set; }

		#endregion

		#region Constructor 

		public PersonAPIManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager, IPersonsManager personsManager)
			: base(userManager, cacheStoreManager, logManager)
		{
			_personManager = ValidateManager(personsManager);
        }

        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity
        {
            if (typeof(BLL_CLASS) == typeof(BLL_Deal)) return _personManager.GetEntityActions(entity as BLL_Person);
            return null;
        }

		#endregion

		#region API Manager

		//protected override T UnitOfWork<T>(long analysisId, Func<T> function, bool persistToDB = false)
		//{
		//    throw new NotImplementedException();
		//}

		#endregion

		#region Person
		public EntityResult<BLL_Person> GetPerson(int personId)
		{
			return RunInContextScope(() =>
			{
				return _personManager.GetPerson(personId);
			});
		}
		public IPaginatedList<BLL_Person> SearchPersons(SearchCriteria criteria)
        {
            return RunInContextScope(() =>
            {
                return _personManager.Search(criteria);
            });
        }

		#endregion

    }
}
