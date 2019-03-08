using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class Cedant : BaseApiModel<BLL_Cedant>
    {
        public Cedant() { }
        public Cedant(BLL_Cedant model) : base(model) { }

        public override BLL_Cedant ToBLLModel()
        {
            BLL_Cedant bLL_cedants = new BLL_Cedant()
            {
                Cedantid = Cedantid,
                Cedant = Name,
                Cedantgroupid = Cedantgroupid,
                Cedantgroupname = Cedantgroupname,
                Locationid = Locationid,
                Locationaddress = Locationaddress,
                Locationcity = Locationcity,
                Locationpostcode = Locationpostcode,
                Locationstate = Locationstate,
                Country = Country,
            };
            return bLL_cedants;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_Cedant model)
        {
            Cedantid = model.Cedantid;
            Name = model.Cedant;
            Cedantgroupid = model.Cedantgroupid;
            Cedantgroupname = model.Cedantgroupname;
            Locationid = model.Locationid;
            Locationaddress = model.Locationaddress;
            Locationcity = model.Locationcity;
            Locationpostcode = model.Locationpostcode;
            Locationstate = model.Locationstate;
            Country = model.Country;
        }
    }
}