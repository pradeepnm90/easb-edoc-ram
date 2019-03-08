using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    public interface ICacheStoreManager
    {
        string BuildKey(string keyName, string keyId);
        string BuildKey(string keyName, int keyId);
        string BuildKey(string keyName, long keyId);
        string BuildKey(string keyName, Enum keyId);
        IEnumerable<string> SearchKeys(string cacheKey);

        T GetItem<T>(string cacheKey, string hashCode = null, bool useDeepCopy = true);
        T GetItem<T>(string cacheKey, Func<string, CacheItem> itemAction, bool useDeepCopy = true);
        T GetItem<T>(string cacheKey, string hashCode, Func<string, CacheItem> itemAction, bool useDeepCopy = true);
        T GetItem<T>(string cacheKey, string hashCode, Func<string, CacheItem> itemAction, Func<T, T> postCachedItemAction, bool useDeepCopy = true);
        IEnumerable<T> SearchItem<T>(string cacheKey, bool useDeepCopy = true);
        IEnumerable<T> SearchItem<T>(string cacheKey, Predicate<T> predicate, bool useDeepCopy = true);
        bool Update(string cacheKey, object item, bool usesHash);
        T Remove<T>(string cacheKey);  
        void FlushCache();
        Dictionary<string, string> GetMetrics();
    }
}
