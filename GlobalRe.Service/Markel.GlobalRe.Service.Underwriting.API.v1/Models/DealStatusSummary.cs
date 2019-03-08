using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public class DealStatusSummary : BaseApiModel<BLL_DealStatusSummary>
    {
        public int? StatusCode { get; set; }
        public string StatusName { get; set; }
        public int? SortOrder { get; set; }
        public int? Count { get; set; }
        public int? WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public IList<DealStatusSummary> StatusSummary { get; set; }

        #region Constructor
        public DealStatusSummary() { }
        public DealStatusSummary(BLL_DealStatusSummary model) : base(model) { }

        #endregion

        public override BLL_DealStatusSummary ToBLLModel()
        {
            BLL_DealStatusSummary bLL_DealStatusSummary = new BLL_DealStatusSummary()
            {

            };

            return bLL_DealStatusSummary;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_DealStatusSummary model)
        {
            StatusName = model.StatusName;
            StatusCode = model.StatusCode;
            SortOrder = model.SortOrder;
            Count = model.Count;
            WorkflowId = model.WorkflowId;
            WorkflowName = model.WorkflowName;
            StatusSummary = model.DealStatusSummary?.Select(summary => new DealStatusSummary(summary)).ToList();
        }
    }
}