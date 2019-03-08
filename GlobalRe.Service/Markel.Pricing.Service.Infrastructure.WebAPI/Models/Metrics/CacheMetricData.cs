namespace Markel.Pricing.Service.Infrastructure.Models.Metrics
{
    public class CacheMetricData
    {
        #region Public Properties

        public string CacheStoreName { get; private set; }
        public string CacheKey { get; private set; }
        public string CacheDetail { get; set; }

        #endregion Public Properties

        #region Constructors

        public CacheMetricData(string cacheStoreName, string cacheKey, string cacheDetail)
        {
            CacheStoreName = cacheStoreName;
            CacheKey = cacheKey;
            CacheDetail = cacheDetail;
        }

        #endregion Consructors
    }
}
