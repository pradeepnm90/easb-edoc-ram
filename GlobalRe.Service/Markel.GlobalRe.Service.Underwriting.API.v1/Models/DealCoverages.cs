using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class DealCoverages : BaseApiModel<BLL_DealCoverages>
    {
        public DealCoverages() { }
        public DealCoverages(BLL_DealCoverages model) : base(model) { }

        public override BLL_DealCoverages ToBLLModel()
        {
            BLL_DealCoverages bLL_DealCoverages = new BLL_DealCoverages()
            {
                Dealnum = DealNumber,
                Cover_Id = (Int32) Cover_Id,
                Cover_Name = Cover_Name
            };

            return bLL_DealCoverages;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_DealCoverages model)
        {
            DealNumber = model.Dealnum;
            Cover_Id = model.Cover_Id;
            Cover_Name = model.Cover_Name;
        }
    }
}