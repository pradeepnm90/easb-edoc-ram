using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    public class CacheStoreManager : ICacheStoreManager
    {
        #region Private Members

        private const string CACHE_ABSOLUTE_EXPIRY_TIME_KEY = "CacheTimerInterval";
        private static IHashProvider HashProvider = null;

        private static object CacheItemLock = new object();

        private static Dictionary<string, string> InitializedEnvironments = new Dictionary<string, string>();

        #endregion Private Members

        #region Public Properties

        public static TimeSpan AbsoluteExpiration { get; private set; }

        #endregion Public Properties

        #region Consrtuctor/Initialization

        /// <summary> Initializes the <see cref="CacheStoreManager"/> class. </summary>
        public CacheStoreManager()
        {
            int expiryTime = 0;
            string refreshInterval = ConfigurationManager.AppSettings[CACHE_ABSOLUTE_EXPIRY_TIME_KEY];
            if (!string.IsNullOrEmpty(refreshInterval))
            {
                int.TryParse(refreshInterval, out expiryTime);
            }
            AbsoluteExpiration = new TimeSpan(expiryTime, 0, 0);
            HashProvider = new HashProvider();

            Initialize(MarkelConfiguration.EnvironmentName);
        }

        /// <summary>
        /// Initializes Cache Store for the specified Environment Name.
        /// </summary>
        /// <param name="environmentName">Name of the Environment.</param>
        /// <returns>Cache Store Name</returns>
        public void Initialize(string environmentName)
        {
            lock (InitializedEnvironments)
            {
                if (!InitializedEnvironments.ContainsKey(environmentName))
                {
                    CacheManager.CreateCacheStore(environmentName);

                    InitializedEnvironments.Add(environmentName, environmentName);
                }
            }
        }

        #endregion Consrtuctor/Initialization

        #region Public Methods

        /// <summary>
        /// Gets the name of the cache store.
        /// </summary>
        /// <returns></returns>
        private string CacheStoreName
        {
            get
            {
                string environmentName = MarkelConfiguration.EnvironmentName;
                if (InitializedEnvironments.ContainsKey(environmentName))
                {
                    return InitializedEnvironments[environmentName];
                }

                throw new Exception(string.Format("Cache Store for Environment '{0}' has not been initialized!", environmentName));
            }
        }

        public T GetItem<T>(string cacheKey, string hashCode = null, bool useDeepCopy = true)
        {
            return GetItem<T>(cacheKey, hashCode, null, null, useDeepCopy);
        }

        public object GetItem(string cacheStoreName, string cacheKey, string hashCode = null, bool useDeepCopy = true)
        {
            // Get cached item if it exists
            if (CacheManager.ContainsKey(cacheStoreName, cacheKey))
            {
                CacheItem cacheItem = CacheManager.Get(cacheStoreName, cacheKey) as CacheItem;
                if (IsLatest(cacheItem, hashCode))
                {
                    object returnItem = cacheItem.Item;

                    // Return Deep Copy or Reference based on configuration
                    if (returnItem != null && useDeepCopy)
                    {
                        returnItem = returnItem.DeepCopy();
                    }

                    return returnItem;
                }
            }

            return null;
        }

        #region Get Item

        /// <summary> Tries to get the cache item from the default cache store. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="itemAction">The item action.</param>
        /// <returns></returns>
        public T GetItem<T>(string cacheKey, Func<string, CacheItem> itemAction, bool useDeepCopy = true)
        {
            return GetItem<T>(cacheKey, null, itemAction, useDeepCopy);
        }

        /// <summary> Tries to get the cache item. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="hashCode">The hash code.</param>
        /// <param name="itemAction">The item action.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"> cacheStoreName or cacheKey </exception>
        public T GetItem<T>(string cacheKey, string hashCode, Func<string, CacheItem> itemAction, bool useDeepCopy = true)
        {
            return GetItem<T>(cacheKey, hashCode, itemAction, null, useDeepCopy);
        }

        public T GetItem<T>(string cacheKey, string hashCode, Func<string, CacheItem> itemAction, Func<T, T> postCachedItemAction, bool useDeepCopy = true)
        {
            return RunWithEntityLock(cacheKey, () =>
            {
                return GetCacheItem(cacheKey, hashCode, itemAction, postCachedItemAction, useDeepCopy);
            });
        }

        #endregion Get Item

        #region Search

        /// <summary>
        /// Returns <see cref="IEnumerable<string>"/> of cache keys which start with/matches specified cache key.
        /// </summary>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <exception cref="System.ArgumentNullException">cacheStoreName or cacheKey</exception>
        public IEnumerable<string> SearchKeys(string cacheKey)
        {
            if (string.IsNullOrEmpty(CacheStoreName)) throw new ArgumentNullException("cacheStoreName");
            if (string.IsNullOrEmpty(cacheKey)) throw new ArgumentNullException("cacheKey");

            return CacheManager.SearchKeys(CacheStoreName, cacheKey);
        }

        /// <summary>
        /// Returns <see cref="IEnumerable<T>"/> of cache items which have a cache key that starts with/matches 
        /// specified cache key.
        /// </summary>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="useDeepCopy">If true, returns a copy of cached item; otherwise, returns reference to item.</param>
        /// <exception cref="System.ArgumentNullException">cacheStoreName or cacheKey</exception>
        public IEnumerable<T> SearchItem<T>(string cacheKey, bool useDeepCopy = true)
        {
            return SearchItem<T>(cacheKey, null, useDeepCopy);
        }

        /// <summary>
        /// Returns <see cref="IEnumerable<T>"/> of cache items which have a cache key that starts with/matches 
        /// specified cache key and predicate result is true.
        /// </summary>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="predicate">Predicate applied to cached item which determines if it is returned in results.</param>
        /// <param name="useDeepCopy">If true, returns a copy of cached item; otherwise, returns reference to item.</param>
        /// <exception cref="System.ArgumentNullException">cacheStoreName or cacheKey</exception>
        public IEnumerable<T> SearchItem<T>(string cacheKey, Predicate<T> predicate, bool useDeepCopy = true)
        {
            var cacheItems = new List<T>();
            var cacheItemKeys = SearchKeys(cacheKey);

            foreach (var key in cacheItemKeys)
            {
                object cachedObject = GetItem(CacheStoreName, key, null, useDeepCopy);
                T cacheItem = (cachedObject != null && cachedObject is T) ? (T)cachedObject : default(T);
                if (predicate == null || (cacheItem != null && predicate(cacheItem)))
                {
                    cacheItems.Add(cacheItem);
                }
            }

            return cacheItems;
        }

        #endregion Get Item

        #region Add Item

        /// <summary> Tries to add a cache item. </summary>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="item">The item.</param>
        /// <param name="hashCode">The hash code.</param>
        /// <returns></returns> <exception cref="System.ArgumentNullException"> cacheStoreName or cacheKey </exception>
        public bool TryAdd(string cacheStoreName, string cacheKey, object item, string hashCode = null)
        {
            bool itemAdded = false;

            if (string.IsNullOrEmpty(cacheStoreName)) throw new ArgumentNullException("cacheStoreName");
            if (string.IsNullOrEmpty(cacheKey)) throw new ArgumentNullException("cacheKey");

            try
            {
                // Get cached item if it exists
                if (!CacheManager.ContainsKey(cacheStoreName, cacheKey))
                {
                    System.Diagnostics.Debug.WriteLine($"CACHE    Add: N[{cacheStoreName}], K[{cacheKey}]");
                    CacheManager.Add(cacheStoreName, AbsoluteExpiration, cacheKey, item, true);
                    itemAdded = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(CultureInfo.CurrentCulture, "Error adding item cache CacheStoreName = {0} CacheKey = {1}", cacheStoreName, cacheKey), ex);
            }

            return itemAdded;
        }

        #endregion Add Item

        #region Update Item

        public bool Update(string cacheKey, object item, bool usesHash)
        {
            bool isItemUpdated = false;

            if (string.IsNullOrEmpty(CacheStoreName)) throw new ArgumentNullException("cacheStoreName");
            if (string.IsNullOrEmpty(cacheKey)) throw new ArgumentNullException("cacheKey");

            try
            {
                // Get cached item if it exists
                if (CacheManager.ContainsKey(CacheStoreName, cacheKey))
                {
                    System.Diagnostics.Debug.WriteLine($"CACHE Update: N[{CacheStoreName}], K[{cacheKey}]");
                    CacheManager.Update(CacheStoreName, AbsoluteExpiration, cacheKey, item, usesHash);
                    isItemUpdated = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(CultureInfo.CurrentCulture, "Error updating item cache CacheStoreName = {0} CacheKey = {1}", CacheStoreName, cacheKey), ex);
            }

            return isItemUpdated;
        }

        #endregion

        #region Remove Item

        /// <summary> Removes the specified item from the cache store. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"> cacheStoreName or cacheKey </exception>
        public T Remove<T>(string cacheKey)
        {
            CacheItem cacheItem = null;
            var returnItem = default(T);

            if (string.IsNullOrEmpty(CacheStoreName)) throw new ArgumentNullException("cacheStoreName");
            if (string.IsNullOrEmpty(cacheKey)) throw new ArgumentNullException("cacheKey");

            try
            {
                if (CacheManager.CacheStores[CacheStoreName] != null)
                {
                    // Get cached item if it exists
                    if (CacheManager.ContainsKey(CacheStoreName, cacheKey))
                    {
                        cacheItem = CacheManager.Get(CacheStoreName, cacheKey) as CacheItem;
                        if (cacheItem != null)
                            returnItem = (T)cacheItem.Item;

                        System.Diagnostics.Debug.WriteLine($"CACHE Remove: N[{CacheStoreName}], K[{cacheKey}]");

                        CacheManager.Remove(CacheStoreName, cacheKey);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(CultureInfo.CurrentCulture, "Error removing item from cache CacheStoreName = {0} CacheKey = {1}", CacheStoreName, cacheKey), ex);
            }

            return returnItem;
        }

        #endregion Remove Item

        #region Flush Cache

        /// <summary> Flushes all caches. </summary>
        public void FlushAllCaches()
        {
            if (CacheManager.CacheStores != null)
            {
                foreach (ICacheStore cacheStore in CacheManager.CacheStores)
                {
                    cacheStore.FlushAll();
                }
            }
        }

        /// <summary>
        /// Flushes the specified cache store.
        /// </summary>
        /// <param name="environmentName">Environment</param>
        public void FlushCache()
        {
            string cacheStoreName = CacheStoreName;

            if (CacheManager.CacheStores[cacheStoreName] != null)
            {
                CacheManager.CacheStores[cacheStoreName].FlushAll();
            }
        }

        #endregion Flush Cache

        #region Public Helper Methods

        /// <summary> Builds the cache key. </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="keyId">The key unique ID.</param>
        /// <returns></returns>
        public string BuildKey(string keyName, string keyId)
        {
            return string.Format("{0}_{1}", keyName, keyId);

            //ETM_FIX: Generation of cache key, it needs to be completely done at caller?
            //System.Reflection.MethodBase.GetCurrentMethod().Name

            //StackTrace st = new StackTrace();
            //StackFrame sf = st.GetFrame(3);
            //return string.Format("{0}_{1}", sf.GetMethod().Name, cacheKey);
        }

        public string BuildKey(string keyName, int keyId)
        {
            return string.Format("{0}_{1}", keyName, keyId);
        }

        public string BuildKey(string keyName, long keyId)
        {
            return string.Format("{0}_{1}", keyName, keyId);
        }

        public string BuildKey(string keyName, Enum keyType)
        {
            return string.Format("{0}_{1}", keyName, keyType);
        }

        /// <summary>
        /// <summary> Builds the cache key. </summary>
        /// </summary>
        /// <param name="keyName">Key Name</param>
        /// <param name="keyType">Key Type</param>
        /// <param name="keyId">Key ID (for specified type)</param>
        /// <param name="subkeyType">Subkey Type</param>
        /// <returns>Composit Key with all parameters</returns>
        public string BuildKey(string keyName, Enum keyType, int keyId, Enum subkeyType)
        {
            return string.Format("{0}_{1}({2}), {3}", keyName, keyType, keyId, subkeyType);
        }

        /// <summary> Determines whether the item in the specified cache store is the latest.  </summary>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="hashCode">The hash code.</param>
        /// <returns> <c>true</c> if the item in the specified cache store is the latest; otherwise, <c>false</c>. </returns>
        /// <exception cref="System.ArgumentNullException"> cacheStoreName or cacheKey </exception>
        public bool IsLatest(string cacheStoreName, string cacheKey, string hashCode, bool checkExpiringTime = true)
        {
            bool isLatest = false;

            if (string.IsNullOrEmpty(cacheStoreName)) throw new ArgumentNullException("cacheStoreName");
            if (string.IsNullOrEmpty(cacheKey)) throw new ArgumentNullException("cacheKey");

            // Get cached item if it exists
            if (CacheManager.ContainsKey(cacheStoreName, cacheKey))
            {
                CacheItem cacheItem = CacheManager.Get(cacheStoreName, cacheKey) as CacheItem;
                isLatest = IsLatest(cacheItem, hashCode, checkExpiringTime);
            }

            return isLatest;
        }

        /// <summary> Creates the hash code for specified object. </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public string CreateHash(object obj)
        {
            return (obj != null) ? HashProvider.CreateHash(obj) : null;
        }

        #endregion Public Helper Methods

        #region Metrics

        public Dictionary<string, string> GetMetrics()
        {
            if (CacheManager.CacheStores == null) return null;

            Dictionary<string, string> metrics = new Dictionary<string, string>();
            ICacheStore cacheStore = CacheManager.CacheStores[CacheStoreName];
            if (cacheStore == null) return metrics;

            foreach (string key in cacheStore.Keys())
            {
                string[] cacheKeyParts = key.Split('_');
                string cacheDetail = cacheKeyParts.Length > 0 ? cacheKeyParts[cacheKeyParts.Length - 1] : "";
                string cacheKey = key.Replace("_" + cacheDetail, "") + "";

                if (metrics.ContainsKey(cacheKey))
                {
                    metrics[cacheKey] += ",\n" + cacheDetail;
                }
                else
                {
                    metrics.Add(cacheKey, cacheDetail);
                }
            }

            return metrics; //.OrderBy(metric => metric.Key);
        }

        #endregion Metrics

        #endregion Public Methods

        #region Private Methods

        private T GetCacheItem<T>(string cacheKey, string hashCode, Func<string, CacheItem> itemAction, Func<T, T> postCachedItemAction, bool useDeepCopy = true)
        {
            var returnItem = default(T);
            CacheItem cacheItem = null;
            bool returnItemFromCache = false;

            if (string.IsNullOrEmpty(CacheStoreName)) throw new ArgumentNullException("cacheStoreName");
            if (string.IsNullOrEmpty(cacheKey)) throw new ArgumentNullException("cacheKey");

            // Get cached item if it exists
            if (CacheManager.ContainsKey(CacheStoreName, cacheKey))
            {
                cacheItem = CacheManager.Get(CacheStoreName, cacheKey) as CacheItem;
            }

            // If item existed in cache and has not expired, then return item; otherwise, execute itemaAction to get item
            if (IsLatest(cacheItem, hashCode))
            {
                #if (DEBUG_CACHE)
                    System.Diagnostics.Debug.WriteLine($"CACHE Return: N[{cacheStoreName}], K[{cacheKey}]");
                #endif
                returnItem = (T)cacheItem.Item;
                returnItemFromCache = true;
            }
            else
            {
                // Item either didn't exist, hash code's don't match or has expired; therefore, execute itemaAction to get item
                CacheItem actionCacheItem = (itemAction != null) ? itemAction((cacheItem != null) ? cacheItem.HashCode : null) : null;

                if (actionCacheItem != null)
                {
                    TimeSpan expiration = actionCacheItem.Expires ?? AbsoluteExpiration;
                    lock (CacheItemLock)
                    {
                        // Update cache
                        if (cacheItem == null)
                        {
                            // itemAction could have added an item to the cache with the same cacheKey; therefore, check cache again to determine how to update cache
                            if (!CacheManager.ContainsKey(CacheStoreName, cacheKey))
                            {
                                System.Diagnostics.Debug.WriteLine($"CACHE    Add: N[{CacheStoreName}], K[{cacheKey}]");
                                returnItem = (T)actionCacheItem.Item;

                                // An expiration time of 0 means we are not using the cache; therefore, we will always be executing itemAction to retrieve the item
                                if (AbsoluteExpiration != TimeSpan.Zero)
                                {
                                    if (actionCacheItem.HashCode != null)
                                        CacheManager.AddWithHashCode(CacheStoreName, expiration, cacheKey, actionCacheItem.Item, actionCacheItem.HashCode);
                                    else
                                        CacheManager.Add(CacheStoreName, expiration, cacheKey, actionCacheItem.Item, true);
                                }
                            }
                            else
                            {
                                #if (DEBUG_CACHE)
                                    System.Diagnostics.Debug.WriteLine($"CACHE Return: N[{cacheStoreName}], K[{cacheKey}]");
                                #endif
                                // Item already added to cache, return action item because it was invoked
                                returnItem = (T)actionCacheItem.Item;
                            }
                        }
                        else
                        {
                            if (actionCacheItem.HashCode == cacheItem.HashCode)
                            {
                                System.Diagnostics.Debug.WriteLine($"CACHE Update: N[{CacheStoreName}], K[{cacheKey}], H:[{actionCacheItem.HashCode}]");

                                //ETM_FIX: This should only update the expiration time (we need a CV update for this)
                                returnItem = (T)cacheItem.Item;
                                if (actionCacheItem.HashCode != null)
                                    CacheManager.UpdateWithHashCode(CacheStoreName, expiration, cacheKey, cacheItem.Item, cacheItem.HashCode);
                                else
                                    CacheManager.Update(CacheStoreName, expiration, cacheKey, cacheItem.Item, true);

                                returnItemFromCache = true;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"CACHE Update: N[{CacheStoreName}], K[{cacheKey}], H:[{actionCacheItem.HashCode}]");

                                returnItem = (T)actionCacheItem.Item;
                                if (actionCacheItem.HashCode != null)
                                    CacheManager.UpdateWithHashCode(CacheStoreName, expiration, cacheKey, actionCacheItem.Item, actionCacheItem.HashCode);
                                else
                                    CacheManager.Update(CacheStoreName, expiration, cacheKey, actionCacheItem.Item, true);
                            }
                        }
                    }
                }
            }

            // Return Deep Copy or Reference based on configuration
            if (returnItem != null && useDeepCopy)
            {
                returnItem = returnItem.DeepCopy();
            }

            // If previously cached, run this code
            if (returnItemFromCache && postCachedItemAction != null)
            {
                returnItem = postCachedItemAction(returnItem);
            }

            return returnItem;
        }

        /// <summary>
        /// Determines whether the specified cache item is latest. If checkExpiringTime is true and the hashCode is null, then only the 
        /// expiring time is used to determine if the cached item is latest
        /// </summary>
        /// <param name="cacheItem">The cache item.</param>
        /// <param name="hashCode">The hash code.</param>
        /// <param name="checkExpiringTime">if set to <c>true</c> [checks expiring time].</param>
        /// <returns> <c>true</c> if the specified cache item is latest; otherwise, <c>false</c>. </returns>
        private bool IsLatest(CacheItem cacheItem, string hashCode, bool checkExpiringTime = true)
        {
            bool isLatest = false;

            if (checkExpiringTime)
                isLatest = (cacheItem != null && DateTime.Now <= cacheItem.Created.Value.Add(cacheItem.Expires.Value) && (hashCode == null || hashCode == cacheItem.HashCode));
            else
                isLatest = (cacheItem != null && hashCode == cacheItem.HashCode);

            return isLatest;
        }

        #endregion Private Methods

        #region Cache Lock

        // The purpose of a Cache Lock is to prevent multiple requests for the same entity (AKA Cache Key)
        // from executing the same lengthy code at the same time. With a cache lock, the first request
        // is used to initialize the data and all subsequent parallel rquests wait for the response.
        // NOTE: This implemntation works with a single server and needs to be re-designed when
        // using a distribuited cache.
        private static object CacheLock = new object();
        private static Dictionary<string, object> CacheLockList = new Dictionary<string, object>();

        private static object GetCacheKeyLock(string cacheKey)
        {
            lock (CacheLock)
            {
                if (!CacheLockList.ContainsKey(cacheKey))
                    CacheLockList.Add(cacheKey, new object());

                return CacheLockList[cacheKey];
            }
        }

        private static void RemoveCacheKeyLock(string cacheKey)
        {
            lock (CacheLock)
            {
                if (CacheLockList.ContainsKey(cacheKey))
                    CacheLockList.Remove(cacheKey);
            }
        }

        private static T RunWithEntityLock<T>(string cacheKey, Func<T> function)
        {
            object cacheKeyLock = GetCacheKeyLock(cacheKey);
            lock (cacheKeyLock)
            {
                try
                {
                    return function();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    RemoveCacheKeyLock(cacheKey);
                }
            }
        }

        #endregion Cache Lock
    }
}
