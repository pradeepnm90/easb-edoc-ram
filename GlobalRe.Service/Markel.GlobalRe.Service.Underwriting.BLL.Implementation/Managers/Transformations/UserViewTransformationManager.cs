using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;




namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class UserViewTransformationManager : BaseManager, IUserViewTransformationManager
    {
        #region Constructor

        public UserViewTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        public IList<BLL_UserView> Transform(IList<grs_VGrsUserView> userview)
        {
            //PS: Refactoring
            List<BLL_UserView> data = new List<BLL_UserView>();
            if (userview.Count > 0)
            {
                userview.GroupBy(a => a.ViewId).ToList().ForEach((sGroup) =>
                {
                    var statusGroupData = sGroup.ToList();
                    statusGroupData.ForEach((summary) =>
                    {
                        data.Add(new BLL_UserView() { ViewId = summary.ViewId, ScreenName = summary.Screenname,
                            UserId = summary.Userid,ViewName=summary.Viewname, Layout = null,
                            Default = summary.Default,
                            UserViewCreationDate = summary.UserViewCreationDate ?? DateTime.MinValue,
                            SortOrder = summary.Sortorder,CustomView = summary.Customview });
                    });
                });
                return data.OrderBy(d => d.CustomView).ThenBy(d => d.SortOrder).ToList(); 
            }
            return null;
        }

        public BLL_UserView Transform(grs_TblUserview dbModel)
        {
            if (dbModel == null)
            {
                return null;
            }
            return new BLL_UserView()
            {
                ViewId = dbModel.ViewId,
                UserId = dbModel.Userid,
                ScreenName = dbModel.Screenname,
                ViewName = dbModel.Viewname,
                Default = dbModel.Default,
                Layout = dbModel.Layout,
                SortOrder = dbModel.Sortorder,
                CustomView = dbModel.Customview
            };
        }
    }
}

