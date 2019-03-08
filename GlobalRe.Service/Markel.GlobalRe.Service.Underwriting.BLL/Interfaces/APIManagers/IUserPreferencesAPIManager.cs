using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IUserPreferencesAPIManager : IBaseManager
    {
        #region Entity Actions

        EntityAction GetEntityActions<BLL_CLASS>(BLL_CLASS entity) where BLL_CLASS : IBusinessEntity;

        #endregion Entity Actions


        #region UserViews
        EntityResult<BLL_UserView> AddUserView(BLL_UserView bLL_UserView);
        EntityResult<BLL_UserView> GetUserViewbyId(int viewId);
        Result DeleteUserViewbyId(int viewId, BLL_UserViewDelete userviewdelete);
        EntityResult<BLL_UserView> UpdateUserView(BLL_UserView bLL_UserView);

        //GRS-741 for ScreenID search
        EntityResult<IEnumerable<BLL_UserView>> GetByUserViewSreenName(string screenname);
        #endregion

	}
}
