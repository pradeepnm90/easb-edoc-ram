using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Data.Cache
{
    /// <summary>
    /// Represents an Exception 
    /// </summary>
    [Serializable]
    public sealed class CacheKeyNotFoundException : Exception
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKeyNotFoundException" /> class.
        /// </summary>
        public CacheKeyNotFoundException(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKeyNotFoundException" /> class.
        /// </summary>
        /// <param name="key">The key of the cache item not found.</param>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheStoreType">Type of the cache store.</param>
        public CacheKeyNotFoundException(string key, string cacheStoreName, Type cacheStoreType) :
            base(FormatExceptionMessage(key, cacheStoreName, cacheStoreType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKeyNotFoundException" /> class.
        /// </summary>
        /// <param name="key">The key of the cache item not found.</param>
        /// <param name="cacheStoreName">Name of the cache store.</param>
        /// <param name="cacheStoreType">Type of the cache store.</param>
        /// <param name="innerException">The inner exception.</param>
        public CacheKeyNotFoundException(string key, string cacheStoreName, Type cacheStoreType, Exception innerException) :
            base(FormatExceptionMessage(key, cacheStoreName, cacheStoreType), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKeyNotFoundException" /> class.
        /// </summary>
        /// <param name="info"> The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context"> The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or System.CacheKeyNotFoundException.HResult is zero (0).</exception>
        private CacheKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context){}
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Formats the exception message using a localized resource file.
        /// </summary>
        /// <param name="key">The key of the cache item not found.</param>
        /// <param name="cacheStoreName">Name of the cache store the exception occured in.</param>
        /// <param name="cacheStoreType">Type of the cache store the exception occured in.</param>
        /// <returns></returns>
        private static string FormatExceptionMessage(string key, string cacheStoreName, Type cacheStoreType)
        {
            return string.Format("CacheKeyNotFound: {0}, Store: {1}, Type: {2}", key, cacheStoreName, cacheStoreType);
        }

        #endregion
    }
}
