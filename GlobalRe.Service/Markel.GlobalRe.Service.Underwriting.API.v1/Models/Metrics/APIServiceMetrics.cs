using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.Metrics;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models.Metrics
{
    public class APIServiceMetrics : IBaseApiModel
    {
        public MetricData ServiceMetrics { get; set; }
        public string DataSource { get; set; }
        public string ServerVersion { get; set; }
        public Dictionary<string, string> CacheMetrics { get; set; }
        public dynamic ServerStorageInfo { get; set; }
    }
}