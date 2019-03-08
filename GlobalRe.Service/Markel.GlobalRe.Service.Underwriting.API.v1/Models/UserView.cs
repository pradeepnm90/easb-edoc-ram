using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class UserView : BaseApiModel<BLL_UserView>
    {
        public UserView() { }
        public UserView(BLL_UserView model) : base(model) { }

        public override BLL_UserView ToBLLModel()
        {
            BLL_UserView bLL_UserView = new BLL_UserView()
            {
                ViewId = ViewId,
                UserId = UserId,
                ViewName = ViewName,
                ScreenName = ScreenName,
                Default = Default,
                Layout = Layout,
                CustomView = CustomView,
                SortOrder = SortOrder
                //UserViewCreationDate=UserViewCreationDate
            };

            return bLL_UserView;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_UserView model)
        {

            ViewId = model.ViewId;
            UserId = model.UserId;
            ViewName = model.ViewName;
            ScreenName = model.ScreenName;
            Default = model.Default;
            Layout = model.Layout;
            CustomView = model.CustomView;
            SortOrder = model.SortOrder;
            //UserViewCreationDate = UserViewCreationDate;
        }
    }
}
