using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    public class NullCacheManager : ICaching
    {
        #region ICaching Members

        public void Add(TimeSpan timeToExpire, string key, object value)
        {
            return;
        }

        public void Add<T>(TimeSpan timeToExpire, string key, T value)
        {
            return;
        }

        public void Add(string key, object value)
        {
            return;
        }

        public void Remove(string key)
        {
            return;
        }

        public bool Contains(string key)
        {
            return false;
        }

        public object Get(string key)
        {
            return null;
        }

        public T Get<T>(string key)
        {
            return default(T);
        }

        public IDictionary<string, object> GetMultiple(string[] keys)
        {
            return new Dictionary<string, object>();
        }

        public IDictionary<string, T> GetMultiple<T>(string[] keys)
        {
            return new Dictionary<string, T>();
        }

        public int CacheLevel { get; set; }
        public TimeSpan TimeToExpire { get; set; }

      
        public void FlushAll()
        {
            return;
        }

        #endregion
    }
}
