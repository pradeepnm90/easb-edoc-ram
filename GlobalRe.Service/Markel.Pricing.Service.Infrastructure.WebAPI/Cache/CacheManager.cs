using Markel.Pricing.Service.Infrastructure.Helpers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    /// <summary>
    /// Represents a CacheStore instance manager that manages multiple instances of CacheStore
    /// </summary>
    public static class CacheManager
    {
        #region Constants

        private const string CacheManagerContainerName = "cacheContainer";
        private const string UnityConfigurationSectionName = "unity";

        #endregion

        #region Private Members

        private static CacheStoreCollection _currentCacheStores;
        private static IUnityContainer _container = null;
        private static readonly object cacheLock = new object();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a collection of CacheStore instances that have been previously initialized 
        /// in the current instance [Unity container] of the CacheManager.
        /// </summary>
        /// <value>The collection of available CacheStore instances.</value>
        public static CacheStoreCollection CacheStores
        {
            get
            {
                if (_currentCacheStores == null)
                {
                    _currentCacheStores = new CacheStoreCollection();
                }
                return _currentCacheStores;
            }
        }

        /// <summary>
        /// Creates an instance of a CacheStore based on the executing assembly's configuration.
        /// </summary>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <returns></returns>
        public static ICacheStore CreateCacheStore(string cacheStoreName)
        {
            if (_container == null)
            {
                _container = new UnityContainer();
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSectionName);
                if (section == null)
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture, "Missing required UnityConfigurationSection: '{0}'", UnityConfigurationSectionName));
                }

                section.Configure(_container, CacheManagerContainerName);
            }

            ICacheStore cacheStore = default(ICacheStore);

            if (CacheStores[cacheStoreName] != null)
            {
                cacheStore = CacheStores[cacheStoreName];
            }
            else
            {
                //try
                //{
                //    cacheStore = _container.Resolve<ICacheStore>(cacheStoreName);
                //}
                //catch (Exception)
                //{
                //    // Do nothing - this is an error trap if unity configuration did not contain the name element
                //    // The instance will be resolved by container.Resolve without the name this time
                //}
                
                if (cacheStore == null)
                {
                    cacheStore = _container.Resolve<ICacheStore>();
                }
                cacheStore.CacheStoreName = cacheStoreName;
                CacheStores.Add(cacheStore);
            }

            return cacheStore;
        }

        #endregion

        #region ICacheStore wrapper implementation

        public static object Add(string cacheStoreName, TimeSpan timeout, string key, object value, bool usesHash) 
        {
            CacheItem cacheItem;
            if (usesHash == true)
            {
                cacheItem = new CacheItem(timeout, GenerateHashCode(value), value);
            }
            else
            {
                cacheItem = new CacheItem(timeout, String.Empty, value);
            }

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Add(timeout, key, cacheItem);
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
            
        }

        //public static T Add<T>(string cacheStoreName, TimeSpan timeout, string key, T value, bool usesHash)
        //{
        //    CacheItem cacheItem;
        //    if (usesHash == true)
        //    {
        //        cacheItem = new CacheItem(DateTime.Now.Add(timeout), GenerateHashCode(value), value);
        //    }
        //    else
        //    {
        //        cacheItem = new CacheItem(DateTime.Now.Add(timeout), String.Empty, value);
        //    }

        //    if (CacheStoreExists(cacheStoreName))
        //    {
        //        CacheStores[cacheStoreName].Add<T>(timeout, key, (T)Convert.ChangeType(cacheItem, typeof(T)));
        //        return CacheStores[cacheStoreName].Get<T>(key);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
        //    }
            
        //}

        public static object Add(string cacheStoreName, string key, object value, bool usesHash)
        {
            CacheItem cacheItem;
            if (usesHash == true)
            {
                cacheItem = new CacheItem(GenerateHashCode(value), value);
            }
            else
            {
                cacheItem = new CacheItem(String.Empty, value);
            }

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Add(key, cacheItem);
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
            
        }

        public static object AddWithHashCode(string cacheStoreName, TimeSpan timeout, string key, object value, string specifiedHashCode)
        {
            CacheItem cacheItem = new CacheItem(timeout, specifiedHashCode, value);

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Add(key, cacheItem);
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }

        }

        //public static T AddWithHashCode<T>(string cacheStoreName, TimeSpan timeout, string key, T value, string specifiedHashCode)
        //{
        //    CacheItem cacheItem = new CacheItem(DateTime.Now.Add(timeout), specifiedHashCode, value);

        //    if (CacheStoreExists(cacheStoreName))
        //    {
        //        CacheStores[cacheStoreName].Add<T>(timeout, key, (T)Convert.ChangeType(cacheItem, typeof(T)));
        //        return CacheStores[cacheStoreName].Get<T>(key);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
        //    }

        //}

        public static object AddWithHashCode(string cacheStoreName, string key, object value, string specifiedHashCode)
        {
            CacheItem cacheItem = new CacheItem(specifiedHashCode, value);

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Add(key, cacheItem);
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }

        }

        public static void Update(string cacheStoreName, string key, object value, bool usesHash)
        {
            CacheItem cacheItem;
            if (usesHash == true)
            {
                cacheItem = new CacheItem(GenerateHashCode(value), value);
            }
            else
            {
                cacheItem = new CacheItem(String.Empty, value);
            }

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Update(key, cacheItem);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
            
        }

        public static void Update(string cacheStoreName, TimeSpan timeout, string key, object value, bool usesHash)
        {
            CacheItem cacheItem;
            if (usesHash == true)
            {
                cacheItem = new CacheItem(timeout, GenerateHashCode(value), value);
            }
            else
            {
                cacheItem = new CacheItem(timeout, String.Empty, value);
            }

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Update(timeout, key, cacheItem);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
            
        }

        public static object UpdateWithHashCode(string cacheStoreName, string key, object value, string specifiedHashCode)
        {
            CacheItem cacheItem = new CacheItem(specifiedHashCode, value);

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Update(key, cacheItem);
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }

        }

        public static object UpdateWithHashCode(string cacheStoreName, TimeSpan timeout, string key, object value, string specifiedHashCode)
        {
            CacheItem cacheItem = new CacheItem(timeout, specifiedHashCode, value);

            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Update(timeout, key, cacheItem);
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }

        }

        public static void Remove(string cacheStoreName, string key)
        {
            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].Remove(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
        }

        public static bool ContainsKey(string cacheStoreName, string key)
        {
            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    return CacheStores[cacheStoreName].ContainsKey(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
        }

        public static IEnumerable<string> SearchKeys(string cacheStoreName, string key)
        {
            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    return CacheStores[cacheStoreName].SearchKeys(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
        }

        public static object Get(string cacheStoreName, string key)
        {
            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    return CacheStores[cacheStoreName].Get(key);
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
        }

        //public static T Get<T>(string cacheStoreName, string key)
        //{
        //    if (CacheStoreExists(cacheStoreName))
        //    {
        //        return CacheStores[cacheStoreName].Get<T>(key);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
        //    }
        //}

        public static void FlushAll(string cacheStoreName)
        {
            if (CacheStoreExists(cacheStoreName))
            {
                lock (cacheLock)
                {
                    CacheStores[cacheStoreName].FlushAll();
                }
            }
            else
            {
                throw new InvalidOperationException("CacheStore " + cacheStoreName + " does not exist");
            }
        }

        #endregion

        #region Internal methods

        internal static string GenerateHashCode(object value)
        {
            HashProvider hashProvider = new HashProvider();
            return hashProvider.CreateHash(value);
        }

        internal static bool CacheStoreExists(string cacheStoreName)
        {
            return CacheStores[cacheStoreName] != null;
        }
        

        #endregion
    }
}
