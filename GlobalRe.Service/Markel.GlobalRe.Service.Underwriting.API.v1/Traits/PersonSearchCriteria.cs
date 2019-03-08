using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Traits;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Traits
{
    public class PersonSearchCriteria : APISearchCriteria
    {

        public int PersonId { get; set; }

        protected override void OnUpdateFilterParams(List<FilterParameter> filterParams)
        {
            filterParams.AddIf("PersonId", PersonId);
        }

    }
}