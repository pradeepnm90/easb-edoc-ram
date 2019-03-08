using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public class SubDivision : BaseApiModel<BLL_SubDivision>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public IList<SubDivision> SubDivisions { get; set; }

        public SubDivision() { }

        public SubDivision(BLL_SubDivision model) : base(model) { }

        public override BLL_SubDivision ToBLLModel()
        {
            BLL_SubDivision bLL_SubDivision = new BLL_SubDivision() { };
            return bLL_SubDivision;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_SubDivision model)
        {
            Id = model.Id;
            Name = model.Name;
            SortOrder = model.SortOrder;
            SubDivisions = model.SubDivisions?.Select(division => new SubDivision(division)).ToList();
        }
    }
}