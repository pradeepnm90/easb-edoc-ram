using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Markel.Pricing.Service.Infrastructure.Models.Metrics
{
    public class WebMethodAggregateData
    {
        #region Properties

        public string WebMethodName { get; private set; }
        public long CompletedRequests { get; private set; }
        public long ActiveRequests { get; private set; }

        public TimeSpan MinProcessTime { get; private set; }
        public double MinProcessTimeMilliseconds { get { return MinProcessTime.TotalMilliseconds; } }

        public TimeSpan MaxProcessTime { get; private set; }
        public double MaxProcessTimeMilliseconds { get { return MaxProcessTime.TotalMilliseconds; } }

        public TimeSpan FirstProcessTime { get; private set; }
        public double FirstProcessTimeMilliseconds { get { return FirstProcessTime.TotalMilliseconds; } }

        public TimeSpan LastProcessTime { get; private set; }
        public double LastProcessTimeMilliseconds { get { return LastProcessTime.TotalMilliseconds; } }

        public TimeSpan TotalProcessingTime { get; private set; }
        public double TotalProcessingTimeMilliseconds { get { return TotalProcessingTime.TotalMilliseconds; } }

        public long? TotalErrors { get; private set; } // 4xx Errors

        public long? TotalFaults { get; private set; } // 5xx Errors

        public DateTime? LastFault { get; private set; }
        public DateTime? LastProcessDateTime { get; private set; }

        public string LastCallingIPAddress { get; private set; }
        public string LastCallingHostFQN { get; private set; }

        #endregion Properties

        #region Constructors

        public WebMethodAggregateData(string webMethodName)
        {
            if (string.IsNullOrEmpty(webMethodName)) throw new Exception("Wen Method Name is a required parameter!");

            WebMethodName = webMethodName;
        }

        #endregion Consructors

        #region Derived Fields

        public TimeSpan AverageProcessingTime
        {
            get
            {
                if (CompletedRequests <= 1)
                {
                    return FirstProcessTime;
                }

                double totalMS = (TotalProcessingTime - FirstProcessTime).TotalMilliseconds / (CompletedRequests - 1);
                return TimeSpan.FromMilliseconds(totalMS);
            }
        }

        public double AverageProcessingTimeMilliseconds { get { return AverageProcessingTime.TotalMilliseconds; } }

        public string LastCallingHostName
        {
            get
            {
                string lastCallingHostFQN = LastCallingHostFQN;
                if (string.IsNullOrEmpty(lastCallingHostFQN) || lastCallingHostFQN == "No such host is known")
                {
                    return LastCallingIPAddress;
                }

                Regex ipRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection result = ipRegex.Matches(lastCallingHostFQN);
                if (result.Count == 1)
                {
                    return result[0].Value;
                }
                else
                {
                    string[] lastHostParts = lastCallingHostFQN.Split('.');
                    return lastHostParts[0];
                }
            }
        }

        #endregion Derived Fields

        #region Public Methods

        internal void StartServiceCall()
        {
            // Increment Active Request Count
            ActiveRequests++;
        }

        internal void EndServiceCall(ServiceMetricStatusEnum status, TimeSpan processTime, DateTime timestamp, string callingIPAddress, string callingHostName)
        {
            // Update Counters
            CompletedRequests++;
            ActiveRequests--;
            TotalProcessingTime += processTime;

            if (MinProcessTime > processTime || MinProcessTime == TimeSpan.Zero)
            {
                MinProcessTime = processTime;
            }

            if (MaxProcessTime < processTime) { MaxProcessTime = processTime; }

            if (FirstProcessTime == TimeSpan.Zero) { FirstProcessTime = processTime; }

            LastProcessTime = processTime;
            LastProcessDateTime = timestamp;
            LastCallingIPAddress = callingIPAddress;
            LastCallingHostFQN = callingHostName;

            if (status == ServiceMetricStatusEnum.Exception)
            {
                TotalFaults = (TotalFaults ?? 0) + 1;
                LastFault = timestamp;
            }
            else if (status == ServiceMetricStatusEnum.Error)
            {
                TotalErrors = (TotalErrors ?? 0) + 1;
            }
        }

        internal void UpdateHostName(Dictionary<string, string> cachedHostNames)
        {
            LastCallingHostFQN = cachedHostNames[LastCallingIPAddress];
        }

        #endregion Public Methods
    }
}
