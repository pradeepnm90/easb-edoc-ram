using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    /// <summary>
    /// Represents an <see cref="T:System.Collections.Generic.ICollection`T"/> of <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> objects.
    /// </summary>
    public sealed class CacheStoreCollection : ICollection<ICacheStore>
    {
        #region Private Members
        /// <summary>
        /// Read-only list of <see cref="T:CoreVelocity.Caching.Core.CacheStore"/>.
        /// </summary>
        private readonly IList<ICacheStore> _innerList;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheStoreCollection" /> class.
        /// </summary>
        public CacheStoreCollection()
        {
            _innerList = new List<ICacheStore>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheStoreCollection" /> class with an <see cref="T:System.Collections.Generic.IEnumerable`T"/> of <see cref="T:CoreVelocity.Caching.Core.CacheStore"/>.
        /// </summary>
        /// <param name="cacheStores">The cache stores.</param>
        public CacheStoreCollection(IEnumerable<ICacheStore> cacheStores) : this()
        {
            this.AddRange(cacheStores);
        }

        #endregion

        #region ICollection<ICacheStore> Members

        /// <summary>
        /// Gets the <see cref="CoreVelocity.Caching.Core.ICacheStore" /> with the specified <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> name.
        /// </summary>
        /// <value></value>
        public ICacheStore this[string cacheStoreName]
        {
            get
            {
                return _innerList.Where(item => item.CacheStoreName == cacheStoreName).FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds a range of <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instances to the collection.
        /// </summary>
        /// <param name="cacheStores">The collection of <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instances to add to the collection.</param>
        public void AddRange(IEnumerable<ICacheStore> cacheStores)
        {
            foreach(ICacheStore cacheStore in cacheStores)
            {
                Add(cacheStore);
            }
        }

        /// <summary>
        /// Adds the specified <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instance.
        /// </summary>
        /// <param name="cacheStore">The <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instance to add to the collection.</param>
        public void Add(ICacheStore cacheStore)
        {
            _innerList.Add(cacheStore);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" /> is read-only. </exception>
        public void Clear()
        {
            _innerList.Clear();
        }

        /// <summary>
        /// Determines whether the CacheStore collection contains the specified <see cref="T:CoreVelocity.Caching.Core.CacheStore"/>.
        /// </summary>
        /// <param name="cacheStore">The <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> to check the collection for.</param>
        /// <returns>true if the <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> or not.</returns>
        public bool Contains(ICacheStore cacheStore)
        {
            return (_innerList.Where(item => cacheStore.CacheStoreName == item.CacheStoreName).FirstOrDefault() != null);
        }

        /// <summary>
        /// Copies the <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> array to the collection.
        /// </summary>
        /// <param name="array">The array of <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instances to copy to the collection.</param>
        /// <param name="arrayIndex">Index of the <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> in the array to copy to the collection.</param>
        public void CopyTo(ICacheStore[] array, int arrayIndex)
        {
            List<ICacheStore> tempList = new List<ICacheStore>();
            tempList.AddRange(_innerList);
            tempList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </returns>
        /// <value>Number of elements contained in the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.</value>
        public int Count
        {
            get
            {
                return _innerList.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />
        /// is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />
        /// is read-only; otherwise, false.</returns>
        /// <value></value>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the specified <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instance from the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </summary>
        /// <param name="item">The <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> to remove.</param>
        /// <returns>true if the <see cref="T:CoreVelocity.Caching.Core.CacheStore"/> instance was removed; otherwise, false.</returns>
        public bool Remove(ICacheStore item)
        {
            ICacheStore cacheStore = _innerList.Where(c => c.CacheStoreName == item.CacheStoreName).FirstOrDefault();

            if (cacheStore != null)
            {
                if (_innerList.Remove(cacheStore))
                {
                    return true;
                }

            }
            return false;
        }

        #endregion

        #region IEnumerable<ICacheStore> Members

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used 
        /// to iterate through the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </returns>
        public IEnumerator<ICacheStore> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be
        /// used to iterate through the <see cref="T:CoreVelocity.Cacing.Core.CacheStoreCollection" />.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
