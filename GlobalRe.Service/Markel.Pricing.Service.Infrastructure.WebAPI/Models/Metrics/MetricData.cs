using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Models.Metrics
{
    public class MetricData
    {
        #region Public Properties

        public string MachineName { get; private set; }
        public int ProcessorCount { get; private set; }

        public DateTime ServerTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        #endregion Public Properties

        #region Private Properties

        private List<ServiceMetricData> ServiceMetricsList;

        #endregion Private Properties

        #region Constructors

        public MetricData()
        {
            MachineName = Environment.MachineName;
            ProcessorCount = Environment.ProcessorCount;
            ServiceMetricsList = new List<ServiceMetricData>();
        }

        #endregion Consructors

        #region Public Methods

        public ServiceMetricData[] ServiceMetricData
        {
            get
            {
                return ServiceMetricsList.OrderBy(m => m.ServiceName).ToArray();
            }
        }

        public ServiceMetricData GetServiceMetricData(string serviceName)
        {
            lock (ServiceMetricsList)
            {
                ServiceMetricData serviceMetricData = ServiceMetricsList.FirstOrDefault(m => m.ServiceName.Equals(serviceName));
                if (serviceMetricData == null)
                {
                    serviceMetricData = new ServiceMetricData(serviceName);
                    ServiceMetricsList.Add(serviceMetricData);
                }
                return serviceMetricData;
            }
        }

        public void UpdateHostNames(Dictionary<string, string> cachedHostNames)
        {
            foreach (var serviceMetric in ServiceMetricsList)
            {
                serviceMetric.UpdateHostNames(cachedHostNames);
            }
        }

        #endregion Public Methods
    }
}
