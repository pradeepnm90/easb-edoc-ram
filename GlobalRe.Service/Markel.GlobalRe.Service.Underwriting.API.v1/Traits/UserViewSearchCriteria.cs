using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Traits;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Traits
{
    public class UserViewSearchCriteria : APISearchCriteria
    {

        public string ScreenName { get; set; }
        public bool DefaultStatus { get; set; }
        public string KeyMember { get; set; }

    }
}