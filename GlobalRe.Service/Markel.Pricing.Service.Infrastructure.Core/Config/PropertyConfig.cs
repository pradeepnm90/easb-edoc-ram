using Markel.Pricing.Service.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Config
{
    internal class PropertyConfig
    {
        internal string Name { get; set; }
        internal ControlTypesEnum controlType { get; set; }
        internal short decimalPoints { get; set; }
        internal short sequenceNumber { get; set; }
        internal ReportlTypesEnum[] reportTypes { get; set; }
    }
}
