using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class ContractTypes : BaseApiModel<BLL_ContractTypes>
    {
        public ContractTypes() { }
        public ContractTypes(BLL_ContractTypes model) : base(model) { }

        public override BLL_ContractTypes ToBLLModel()
        {
            BLL_ContractTypes bLL_contractTypes = new BLL_ContractTypes()
            {
                name = name,
                value = value,
                group = group,
                isActive = isActive
            };

            return bLL_contractTypes;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_ContractTypes model)
        {
            name = model.name;
            value = model.value;
            group = model.group;
            isActive = model.isActive;
        }

    }
}