using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    public enum OutputFormat
    {
        [Description("JSON")]
        JSON = 0,
        [Description("CSV")]
        CSV = 1
    }
}
