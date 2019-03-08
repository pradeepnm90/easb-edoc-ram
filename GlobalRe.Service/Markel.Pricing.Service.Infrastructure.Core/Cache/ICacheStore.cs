using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    /// <summary>
    /// This interface defines a contract for all Caching Stores to implement.
    /// </summary>
    public interface ICacheStore : IDisposable
    {
        /// <summary>
        /// Gets or sets whether the ICacheStore is initialized or not.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets or sets the name of the cache store to use if one is needed for the specified ICacheStore.
        /// </summary>
        string CacheStoreName { get; set; }

        /// <summary>
        /// Gets a list of all Keys in the Cache Store
        /// </summary>
        List<string> Keys();

        /// <summary>
        /// Adds an object to the cache for a limited time.
        /// </summary>
        /// <param name="timeout">Time the object should be kept, after which it can be disposed.</param>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        void Add(TimeSpan timeout, string key, object value);

        /// <summary>
        /// Adds an object to the cache and uses generic type.
        /// </summary>
        /// <typeparam name="T">Type of the object to be stored in the cache</typeparam>
        /// <param name="timeout">Time the object should be kept, after which it can be disposed.</param>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        void Add<T>(TimeSpan timeout, string key, T value);

        /// <summary>
        /// Adds an object to the cache. It is upto the implementer on how long it wishes to keep the item in cache.
        /// Typically it will be govered by the TimeToExpire property being set.
        /// </summary>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        void Add(string key, object value);

        /// <summary>
        /// Updates an object in cacue.
        /// </summary>
        /// <param name="key">The key of the item to update.</param>
        /// <param name="value">The value to update it to.</param>
        void Update(string key, object value);

        /// <summary>
        /// Updates an object and changes the timeout.
        /// </summary>
        /// <param name="timeout">The timeout to change the cached item to.</param>
        /// <param name="key">The key of the item to update.</param>
        /// <param name="value">The value to update it to.</param>
        void Update(TimeSpan timeout, string key, object value);

        /// <summary>
        /// Removes an object from the cache using the specified key.
        /// </summary>
        /// <param name="key">Key which will be used to remove the same object from cache.</param>
        void Remove(string key);

        /// <summary>
        /// Checks whether the cache contains the specified key or not.
        /// </summary>
        /// <param name="key">Key which will be used to check whether the cache contains it or not.</param>
        /// <returns> true if the cache contains the specified key; otherwise, false.</returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Returns all cache keys that start with/matches the specified cache key.
        /// </summary>
        /// <param name="key">Key checked whether cache contains it or not.</param>
        /// <returns><see cref="IEnumerable<string>"/> of cache keys that have the specified key prefix.</returns>
        IEnumerable<string> SearchKeys(string key);

        /// <summary>
        /// Retrives an object from cache using the specified key.
        /// </summary>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <returns>Object retrieved from the cache based on the key specified.</returns>
        object Get(string key);

        /// <summary>
        /// Retrives an object from cache using the specified key.
        /// </summary>
        /// <typeparam name="T">Type of object to fetch from the cache.</typeparam>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <returns>Object retrieved from the cache based on the key specified cast to the type specified.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Retrieves multiple items from the LocalCacheStore. (not currently implemented)
        /// </summary>
        /// <param name="keys">Array of CacheItem's keys to retrieve from the LocalCacheStore.</param>
        /// <returns>Dictionary string,object containing the CacheItems matching the specified keys in the key array.</returns>
        IDictionary<string, object> GetMultiple(string[] keys);

        /// <summary>
        /// Retrieves multiple items from the LocalCacheStore. (not currently implemented)
        /// </summary>
        /// <param name="keys">Array of <see cref="CacheItem"/>'s keys to retrieve from the <see cref="LocalCacheStore"/>.</param>
        /// <returns>Dictionary string,T containing the <see cref="CacheItem"/> matching the specified keys in the key array.</returns>
        IDictionary<string, T> GetMultiple<T>(string[] keys);

        /// <summary>
        /// Initializes the instance of the ICacheStore if there is anything that needs to be initialized.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Implements code that should flush all items from the specified ICacheStore.
        /// </summary>
        void FlushAll();
    }
}
