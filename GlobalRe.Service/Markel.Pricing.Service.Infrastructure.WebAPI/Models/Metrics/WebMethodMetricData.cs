using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Models.Metrics
{
    public class WebMethodMetricData
    {
        #region Properties

        public string ServiceName { get; private set; }
        public string WebMethodName { get; private set; }
        public DateTime StartTime { get; private set; }

        #endregion Properties

        #region Constructors

        public WebMethodMetricData(string serviceName, string webMethodName)
        {
            ServiceName = serviceName;
            WebMethodName = webMethodName;
            StartTime = DateTime.Now;
        }

        #endregion Consructors
    }
}
