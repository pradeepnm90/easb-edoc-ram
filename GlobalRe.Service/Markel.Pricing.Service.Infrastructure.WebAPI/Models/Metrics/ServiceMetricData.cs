using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Models.Metrics
{
    public class ServiceMetricData
    {
        #region Public Properties
        public string ServiceName { get; private set; }
        public DateTime ServiceStartDate { get; private set; }
        #endregion Public Properties

        #region Private Properties
        private List<WebMethodAggregateData> WebMethodMetricsList;
        #endregion Private Properties

        #region Constructors & Initialization

        public ServiceMetricData(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName)) throw new Exception("Service Name is a required parameter!");

            ServiceStartDate = DateTime.Now;
            ServiceName = serviceName;

            // Initialize Counters
            WebMethodMetricsList = new List<WebMethodAggregateData>();
        }

        #endregion Constructors & Initialization

        #region Public Methods

        internal void StartServiceCall(string webMethodName)
        {
            // Initialize Counter for Method
            lock (WebMethodMetricsList)
            {
                WebMethodAggregateData serviceMetricData = WebMethodMetricsList.FirstOrDefault(m => m.WebMethodName.Equals(webMethodName));
                if (serviceMetricData == null)
                {
                    serviceMetricData = new WebMethodAggregateData(webMethodName);
                    WebMethodMetricsList.Add(serviceMetricData);
                }
                serviceMetricData.StartServiceCall();
            }
        }

        internal void EndServiceCall(string methodName, ServiceMetricStatusEnum status, TimeSpan processTime, DateTime timestamp, string callingIPAddress, string callingHostName)
        {
            // Update Counters
            lock (WebMethodMetricsList)
            {
                WebMethodAggregateData serviceMetricData = WebMethodMetricsList.FirstOrDefault(m => m.WebMethodName.Equals(methodName));
                serviceMetricData.EndServiceCall(status, processTime, timestamp, callingIPAddress, callingHostName);
            }
        }

        internal void UpdateHostNames(Dictionary<string, string> cachedHostNames)
        {
            var uninitializedHosts = WebMethodMetricsList.Where(metrics => !string.IsNullOrEmpty(metrics.LastCallingIPAddress) && metrics.LastCallingIPAddress.Equals(metrics.LastCallingHostFQN));
            foreach (var metric in uninitializedHosts)
            {
                metric.UpdateHostName(cachedHostNames);
            }
        }

        public WebMethodAggregateData[] WebMethodMetrics
        {
            get
            {
                return WebMethodMetricsList.OrderBy(m => m.WebMethodName).ToArray();
            }
        }

        #endregion Public Methods
    }
}
