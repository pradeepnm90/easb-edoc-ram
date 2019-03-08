using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class ChkCategoryTree : BaseApiModel<BLL_ChkCategoryTree>
    {

        public ChkCategoryTree() { }

        public ChkCategoryTree(BLL_ChkCategoryTree model) : base(model) { }

        public override BLL_ChkCategoryTree ToBLLModel()
        {
            BLL_ChkCategoryTree BLL_ChkCategoryTree = new BLL_ChkCategoryTree() { };
            return BLL_ChkCategoryTree;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_ChkCategoryTree model)
        {

            DealNumber = model.DealNumber;
            EntityNum = model.EntityNum;
            CategoryID = model.CategoryID;
            CategoryName = model.CategoryName;
            CatOrder = model.CatOrder;
            Checklists = model.Checklists?.Select(c => new CheckListTree(c)).ToList();
        }
    }
}
