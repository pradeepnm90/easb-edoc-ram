using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Models.Metrics;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    public class ServiceMetrics
    {
        #region Global Metrics

        private static MetricData ServerMetricData = new MetricData();

        #endregion Global Metrics

        #region Service Metrics

        private ServiceMetricData CurrentServiceMetrics { get; set; }
        private Dictionary<string, DateTime> CurrentWebMethodCallStart { get; set; }

        #endregion Service Metrics

        #region Constructors & Initialization

        public ServiceMetrics(string serviceName)
        {
            CurrentWebMethodCallStart = new Dictionary<string, DateTime>();
            CurrentServiceMetrics = ServerMetricData.GetServiceMetricData(serviceName);
        }

        #endregion Constructors & Initialization

        #region Public Methods

        /// <summary>
        /// Starts metrics for a given service. Eg. Process Time and Active Requests.
        /// This method knows who calls it.
        /// <param name="webMethodName">Web Method Name</param>
        /// </summary>
        public void StartServiceCall(string webMethodName)
        {
            DateTime timestamp = DateTime.Now;

            lock (CurrentWebMethodCallStart)
            {
                if (!CurrentWebMethodCallStart.ContainsKey(webMethodName))
                {
                    CurrentWebMethodCallStart.Add(webMethodName, timestamp);
                }
                else
                {
                    CurrentWebMethodCallStart[webMethodName] = timestamp;
                }

                CurrentServiceMetrics.StartServiceCall(webMethodName);
            }
        }

        /// <summary>
        /// Ends metrics for a given service. This method also tracks the service status (any exceptions) and the number of items processed.
        /// </summary>
        /// <param name="methodName">Method Name</param>
        /// <param name="exception">Exception (if one occured)</param>
        /// <param name="itemsProcessed">Number of Items Processed</param>
        public void EndServiceCall(string methodName, Exception exception, string callingIPAddress = null)
        {
            DateTime timestamp = DateTime.Now;

            if (string.IsNullOrEmpty(callingIPAddress))
            {
                callingIPAddress = GetCallingIPAddress();
            }

            ServiceMetricStatusEnum status = ServiceMetricStatusEnum.Success;
            if (exception != null)
            {
                if (exception is APIException)
                    status = ServiceMetricStatusEnum.Error;
                else
                    status = ServiceMetricStatusEnum.Exception;
            }

            lock (CurrentWebMethodCallStart)
            {
                TimeSpan processTime = timestamp.Subtract(CurrentWebMethodCallStart[methodName]);
                CurrentServiceMetrics.EndServiceCall(methodName, status, processTime, timestamp, callingIPAddress, GetHostName(callingIPAddress));
            }
        }

        public static MetricData GetMetrics()
        {
            // Resolve Host Name
            ServerMetricData.UpdateHostNames(CachedHostNames);

            return ServerMetricData;
        }

        #endregion Service Metrics

        #region Helper Methods

        private static string GetCallingIPAddress()
        {
            try
            {
                OperationContext currentOperationContext = OperationContext.Current;

                if (currentOperationContext == null) return "LOCAL";

                MessageProperties properties = currentOperationContext.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                return endpoint.Address;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static Dictionary<string, string> CachedHostNames = new Dictionary<string, string>();
        internal static string GetHostName(string ipAddress)
        {
            lock (CachedHostNames)
            {
                if (!CachedHostNames.ContainsKey(ipAddress))
                {
                    CachedHostNames.Add(ipAddress, ipAddress);

                    if (!ipAddress.Equals("N/A") && !ipAddress.Equals("LOCAL"))
                    {
                        new Task(() =>
                        {
                            try
                            {
                                IPHostEntry ipToDomainName = Dns.GetHostEntry(ipAddress);
                                CachedHostNames[ipAddress] = ipToDomainName.HostName;
                            }
                            catch (Exception ex)
                            {
                                CachedHostNames[ipAddress] = ex.Message;
                            }
                        }).Start();
                    }
                }

                return CachedHostNames[ipAddress];
            }
        }

        #endregion Helper Methods
    }
}