using Markel.Pricing.Service.Infrastructure.Helpers;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Data.Helpers
{
    public interface IFilter
    {
        IList<FilterCondition> FilterConditionList { get; }
    }
}
