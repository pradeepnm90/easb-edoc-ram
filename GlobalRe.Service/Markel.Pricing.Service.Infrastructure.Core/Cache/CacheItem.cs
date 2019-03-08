using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    /// <summary>
    /// Represents an item that will be cached along with the
    /// date to expire the item (null if never expires.)
    /// </summary>
    [Serializable]
    public sealed class CacheItem
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the time the CacheItem to expire.
        /// </summary>
        public TimeSpan? Expires { get; set; }

        /// <summary>
        /// Gets the time the CacheItem was created.
        /// </summary>
        public DateTime? Created { get; private set; }

        /// <summary>
        /// Gets or sets Hashcode for the cache item
        /// </summary>
        public string HashCode { get; set; }

        /// <summary>
        /// Gets or sets the object to cache.
        /// </summary>
        public object Item { get; set; }

        #endregion

        #region Constructor

        public CacheItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem" /> class.
        /// </summary>
        /// <param name="item">The item to store in the CacheManager.</param>
        public CacheItem(object item)
        {
            Created = DateTime.Now;
            Item = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem" /> class.
        /// </summary>
        /// <param name="hashcode">Hashcode for the cache item</param>
        /// <param name="item">The item to store in the CacheManager.</param>
        public CacheItem(string hashcode, object item)
        {
            Created = DateTime.Now;
            HashCode = hashcode;
            Item = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem" /> class.
        /// </summary>
        /// <param name="expiration">The Timespan the item to cache will expire and be removed from the CacheManager.</param>
        /// <param name="hashcode">Hashcode for the cache item</param>
        /// <param name="item">The item to store in the CacheManager.</param>
        public CacheItem(TimeSpan? expiration, string hashcode, object item)
        {
            Created = DateTime.Now;
            Expires = expiration;
            HashCode = hashcode;
            Item = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem" /> class.
        /// </summary>
        /// <param name="expires">The DateTime the item to cache will expire and be removed from the CacheManager.</param>
        /// <param name="hashcode">Hashcode for the cache item</param>
        /// <param name="item">The item to store in the CacheManager.</param>
        public CacheItem(DateTime? expires, string hashcode, object item)
        {
            Created = DateTime.Now;
            Expires = (expires ?? Created).Value.Subtract(Created.Value);
            HashCode = hashcode;
            Item = item;
        }

        #endregion
    }
}
