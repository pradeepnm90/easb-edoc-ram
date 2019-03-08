using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    /// <summary>
    /// This interface defines a contract for all Caching Managers to implement.
    /// </summary>
    public interface ICaching
    {
        /// <summary>
        /// Adds an object to the cache for a limited time.
        /// </summary>
        /// <param name="timeToExpire">Time the object should be kept, after which it can be disposed.</param>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        void Add(TimeSpan timeToExpire, string key, object value);

        /// <summary>
        /// Adds an object to the cache and uses generic type.
        /// </summary>
        /// <typeparam name="T">Type of the object to be stored in the cache</typeparam>
        /// <param name="timeToExpire">Time the object should be kept, after which it can be disposed.</param>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        void Add<T>(TimeSpan timeToExpire, string key, T value);

        /// <summary>
        /// Adds an object to the cache. It is upto the implementer on how long it wishes to keep the item in cache.
        /// Typically it will be govered by the TimeToExpire property being set.
        /// </summary>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        void Add(string key, object value);
        
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
        bool Contains(string key);

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
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<string, object> GetMultiple(string[] keys);
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<string, T> GetMultiple<T>(string[] keys);

        /// <summary>
        /// 
        /// </summary>
        int CacheLevel { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        TimeSpan TimeToExpire { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void FlushAll();
    }
}
