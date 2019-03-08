using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    public enum TransactionResult
    {
        [Description("Success")]
        Success = 0,
        [Description("Info")]
        Info = 1,
        [Description("Warning")]
        Warning = 2,
        [Description("Error")]
        Error = 4
    }
}
