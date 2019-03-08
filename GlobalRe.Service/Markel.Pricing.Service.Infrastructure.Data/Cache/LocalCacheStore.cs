using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Data.Cache
{
    /// <summary>
    /// Represents a CacheManager that utilizes a static local member for storing
    /// cache within an application. This manager should be used mainly for 
    /// testing and debugging.
    /// </summary>
    public sealed class LocalCacheStore : ICacheStore
    {
        #region Static Members
        /// <summary>
        /// Backing field for LocalCacheStorage.
        /// </summary>
        private Dictionary<string, object> _localCacheStorage;

        private Dictionary<string, object> LocalCacheStorage
        {
            get
            {
                if (_localCacheStorage == null)
                {
                    _localCacheStorage = new Dictionary<string, object>();
                }
                return _localCacheStorage;
            }
            set
            {
                _localCacheStorage = value;
            }
        }

        public List<string> Keys()
        {
            return new List<string>(LocalCacheStorage.Keys);
        }

        #endregion

        #region ICacheManager Members

        #region Public Properties

        /// <summary>
        /// Gets whether or not the LocalCacheManager has been initialized.
        /// </summary>
        public bool IsInitialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the name of the cache store to use if one is needed for the specified CacheManager.
        /// </summary>
        /// <value></value>
        public string CacheStoreName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the instance of the CacheManager if there is anything that needs to be initialized.
        /// </summary>
        public void Initialize()
        {
            IsInitialized = true;
        }

        /// <summary>
        /// Adds an object to the cache for a limited time.
        /// </summary>
        /// <param name="timeout">Time the object should be kept, after which it can be disposed.</param>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        public void Add(TimeSpan timeout, string key, object value)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            Asserter.AssertIsNotNull("value", value);

            LocalCacheStorage.Add(key, value);
        }

        /// <summary>
        /// Adds an object to the cache and uses generic type.
        /// </summary>
        /// <param name="timeout">Time the object should be kept, after which it can be disposed.</param>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        public void Add<T>(TimeSpan timeout, string key, T value)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            Asserter.AssertIsNotNull("value", value);

            LocalCacheStorage.Add(key, value);
        }

        /// <summary>
        /// Adds an object to the cache. It is upto the implementer on how long it wishes to keep the item in cache.
        /// Typically it will be govered by the TimeToExpire property being set.
        /// </summary>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <param name="value">The instance of the object that should be stored in cache.</param>
        public void Add(string key, object value)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            Asserter.AssertIsNotNull("value", value);
            
            LocalCacheStorage.Add(key, value);
        }

        /// <summary>
        /// Updates an object in cache.
        /// </summary>
        /// <param name="key">The key of the item to update.</param>
        /// <param name="value">The value to update it to.</param>
        public void Update(string key, object value)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            Asserter.AssertIsNotNull("value", value);
            object returnValue;

            if (LocalCacheStorage.TryGetValue(key, out returnValue))
            {
                LocalCacheStorage[key] = value;
            }
            else
            {
                throw new CacheKeyNotFoundException(key, CacheStoreName, GetType());
            }
        }

        /// <summary>
        /// Updates an object in cache.
        /// </summary>
        /// <param name="key">The key of the item to update.</param>
        /// <param name="value">The value to update it to.</param>
        public void Update(TimeSpan timeout, string key, object value)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            Asserter.AssertIsNotNull("value", value);
            object returnValue;

            if (LocalCacheStorage.TryGetValue(key, out returnValue))
            {
                LocalCacheStorage[key] = value;
            }
            else
            {
                throw new CacheKeyNotFoundException(key, CacheStoreName, GetType());
            }
        }

        /// <summary>
        /// Removes an object from the cache using the specified key.
        /// </summary>
        /// <param name="key">Key which will be used to remove the same object from cache.</param>
        public void Remove(string key)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            LocalCacheStorage.Remove(key);
        }

        /// <summary>
        /// Checks whether the cache contains the specified key or not.
        /// </summary>
        /// <param name="key">Key which will be used to check whether the cache contains it or not.</param>
        /// <returns>true if the cache contains the specified key; otherwise, false.</returns>
        public bool ContainsKey(string key)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            return LocalCacheStorage.ContainsKey(key);
        }

        /// <summary>
        /// Returns all cache keys that start with the specified cache key.
        /// </summary>
        /// <param name="key">Key which will be used to check whether the cache contains it or not.</param>
        /// <returns>IEnumerable<string> </string> of cache keys that have the specified key prefix.</returns>
        public IEnumerable<string> SearchKeys(string key)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            return LocalCacheStorage.Keys.Where(k => k.StartsWith(key)).ToList();
        }

        /// <summary>
        /// Retrives an object from cache using the specified key.
        /// </summary>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <returns>Object retrieved from the cache based on the key specified.</returns>
        public object Get(string key)
        {
            Asserter.AssertIsNotNullOrEmptyString("key", key);
            object value = null;
            object returnValue;
            if (LocalCacheStorage.TryGetValue(key, out returnValue))
            {
                value = returnValue;
            }

            return value;
        }

        /// <summary>
        /// Retrives an object from cache using the specified key.
        /// </summary>
        /// <param name="key">Key which will be used to retrieve the same object.</param>
        /// <returns>
        /// Object retrieved from the cache based on the key specified cast to the type specified.
        /// </returns>
        public T Get<T>(string key)
        {
            return (T)Get(key);
        }

        /// <summary>
        /// Clears the entire LocalCacheStore of all items.
        /// </summary>
        public void FlushAll()
        {
            LocalCacheStorage.Clear();
        }

        /// <summary>
        /// Retrieves multiple items from the LocalCacheStore. (not currently implemented)
        /// </summary>
        /// <param name="keys">Array of CacheItem's keys to retrieve from the LocalCacheStore.</param>
        /// <returns>Dictionary string,object containing the CacheItems matching the specified keys in the key array.</returns>
        public IDictionary<string, object> GetMultiple(string[] keys)
        {
            throw new NotImplementedException("Getting mulitple dictionary objects is not allowed.");
        }
        
        /// <summary>
        /// Retrieves multiple items from the LocalCacheStore. (not currently implemented)
        /// </summary>
        /// <param name="keys">Array of CacheItem's keys to retrieve from the LocalCacheStore.</param>
        /// <returns>Dictionary string,T containing the CacheItems matching the specified keys in the key array.</returns>
        public IDictionary<string, T> GetMultiple<T>(string[] keys)
        {
            throw new NotImplementedException("Getting mulitple dictionary objects is not allowed.");
        }

        #endregion

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            LocalCacheStorage = null;
        }

        #endregion     
    }
}
