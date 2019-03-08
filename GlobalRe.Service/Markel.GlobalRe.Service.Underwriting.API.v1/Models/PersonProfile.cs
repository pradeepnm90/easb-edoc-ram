using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class PersonProfile : BaseApiModel<BLL_PersonProfile>
    {
        //public IList<int> DefaultSubDivisions { get; set; }

        public PersonProfile() { }

        public PersonProfile(BLL_PersonProfile model) : base(model) { }

        public override BLL_PersonProfile ToBLLModel()
        {
            BLL_PersonProfile bLL_PersonProfile = new BLL_PersonProfile() { };
            return bLL_PersonProfile;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_PersonProfile model)
        {
            DefaultSubDivisions = model.DefaultSubDivisions?.ToList();
        }
    }
}