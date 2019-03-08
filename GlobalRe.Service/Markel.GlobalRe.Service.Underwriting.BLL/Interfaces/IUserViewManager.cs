using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{

    public interface IUserViewManager : ISearchableManager<BLL_UserView>
    {
        EntityAction GetEntityActions(BLL_UserView entity);
        EntityResult<BLL_UserView> GetUserViewByID(int viewID);
        Result DeleteUserViewByID(int viewID, BLL_UserViewDelete userviewdelete);
        EntityResult<IEnumerable<BLL_UserView>> GetUserViews(int userId, string screenName);
        EntityResult<BLL_UserView> AddUserView(BLL_UserView bLL_UserView);
        EntityResult<BLL_UserView> UpdateUserView(BLL_UserView bLL_UserView);
        //GRS-741 for ScreenID search
       EntityResult<IEnumerable<BLL_UserView>> GetByUserViewSreenName(string screenname);
    }
}
