using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_DealStatusSummary : BaseGlobalReBusinessEntity
    {
        public int? StatusCode { get; set; }
        public string StatusName { get; set; }
        public int? SortOrder { get; set; }
        public int? Count { get; set; }
        public int? WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public List<BLL_DealStatusSummary> DealStatusSummary { get; set; }

    }
}
