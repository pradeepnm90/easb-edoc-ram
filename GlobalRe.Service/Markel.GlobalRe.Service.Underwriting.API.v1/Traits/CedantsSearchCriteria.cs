using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Traits;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Traits
{
    public class CedantsSearchCriteria : APISearchCriteria
    {
        public string CedantName { get; set; }
        public string ParentGroupName { get; set; }
        public string CedantId { get; set; }
        public string ParentGroupId { get; set; }
        public string LocationId { get; set; } 
    }
}