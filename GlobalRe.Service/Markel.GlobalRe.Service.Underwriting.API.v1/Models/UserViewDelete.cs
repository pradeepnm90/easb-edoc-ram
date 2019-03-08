using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class UserViewDelete : BaseApiModel<BLL_UserViewDelete>
    {
        public UserViewDelete() { }
        public UserViewDelete(BLL_UserViewDelete model) : base(model) { }

        public override BLL_UserViewDelete ToBLLModel()
        {
            BLL_UserViewDelete bLL_UserView = new BLL_UserViewDelete()
            {
                DefaultStatus = Default,
                KeyMember = KeyMember,
                ScreenName = ScreenName
            };

            return bLL_UserView;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_UserViewDelete model)
        {

            ScreenName = model.ScreenName;
            Default = model.DefaultStatus;
            KeyMember = model.KeyMember;
        }
    }
}
