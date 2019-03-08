using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class UserPreferencesAPIManager : BaseUnitOfWorkManager, IUserPreferencesAPIManager
    {

        #region Repositories & Managers
        private IUserViewManager _userViewManager { get; set; }

		#endregion

		#region Constructor 

		 //GRS-48 User Views
        public UserPreferencesAPIManager(IUserManager userManager,
                                  ICacheStoreManager cacheStoreManager,
                                  ILogManager logManager,
                                  IUserViewManager userViewManager)
       : base(userManager, cacheStoreManager, logManager)
        {
            _userViewManager = ValidateManager(userViewManager);
        }
        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity
        {
            if (typeof(BLL_CLASS) == typeof(BLL_UserView)) return _userViewManager.GetEntityActions(entity as BLL_UserView);
            return null;
        }

        #endregion

        #region API Manager

        //protected override T UnitOfWork<T>(long analysisId, Func<T> function, bool persistToDB = false)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region Deals 

        public EntityResult<BLL_UserView> AddUserView(BLL_UserView bLL_UserView)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.AddUserView(bLL_UserView);
            });
        }

        public EntityResult<BLL_UserView> UpdateUserView(BLL_UserView bLL_UserView)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.UpdateUserView(bLL_UserView);
            });
        }

        public EntityResult<BLL_UserView> GetUserViewbyId(int viewId)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.GetUserViewByID(viewId);
            });
        }

        //GRS-741 for ScreenID search
        public EntityResult<IEnumerable<BLL_UserView>> GetByUserViewSreenName(string screenname)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.GetByUserViewSreenName(screenname);
                //return null;
            });
        }

        public Result DeleteUserViewbyId(int viewId, BLL_UserViewDelete userviewdelete)
        {
            return RunInContextScope(() =>
            {
                return _userViewManager.DeleteUserViewByID(viewId, userviewdelete);
            });
        }

        #endregion

    }
}
