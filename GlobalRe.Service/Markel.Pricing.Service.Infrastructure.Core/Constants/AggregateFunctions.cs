using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Constants
{

    /// <summary>
    /// Enum that sets the aggreate functions like SUM,AVG 
    /// </summary>   
    public enum AggregateFunctions
    {
        NONE = 0,
        SUM = 1,
        AVG = 2,
        COUNT = 3
    }
}
