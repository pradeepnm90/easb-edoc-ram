using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents a collection of <see cref="T:CoreVelocity.Core.Data.SortedField"/> objects used create
    /// an order by statement on specified fields.
    /// </summary>
    public sealed class SortedFieldCollection : ICollection<SortedField>
    {
        #region Private Members

        /// <summary>
        /// The internal readonly list that contains the collection of <see cref="CoreVelocity.Data.SortedField"/> objects.
        /// </summary>
        private readonly IList<SortedField> _innerList;
        
        #endregion 

        #region Constructor
        /// <summary>
        /// Creates a new instance of SortedFieldCollection.
        /// </summary>
        public SortedFieldCollection()
        {
            _innerList = new List<SortedField>();
        }

        #endregion

        #region ICollection<SortedField> Members

        /// <summary>
        /// Gets the <see cref="T:CoreVelocity.DataDataAccess.SortedField" /> with the specified <see cref="CoreVelocity.DataAccess.SortedField.PropertyName"/>.
        /// </summary>
        /// <value></value>
        public SortedField this[string propertyName]
        {
            get
            {
                return _innerList.Where(item => item.PropertyName.ToUpper() == propertyName.ToUpper()).FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds a range of <see cref="T:CoreVelocity.DataAccess.SortedField"/> instances to the collection.
        /// </summary>
        /// <param name="SortedFields">The collection of <see cref="T:CoreVelocity.DataAccess.SortedField"/> instances to add to the collection.</param>
        public void AddRange(IEnumerable<SortedField> sortedFields)
        {
            foreach (SortedField sortedField in sortedFields)
            {
                Add(sortedField);
            }
        }
        

        /// <summary>
        /// Adds a <see cref="T:CoreVelocity.DataAccess.SortedField"/> to the collection using the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of object to find the property on using the specified linq expression.</typeparam>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="sortOrder">The direction to sort by.</param>
        public void Add(string propertyName, SortOrderType sortOrder)
        {
            Add(new SortedField(propertyName, sortOrder));
        }
        /// <summary>
        /// Adds a SortedField to the collection using the specified parameters.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="sortOrder">The direction to sort by.</param>
        /// <param name="sortOrderIndex">The order to sort by.</param>
        public void Add(string propertyName, SortOrderType sortOrder, int sortOrderIndex)
        {
            Add(new SortedField(propertyName, sortOrder, sortOrderIndex));
        }
        /// <summary>
        /// Adds a SortedField to the collection using the specified parameters.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="sortOrder">The direction to sort by.</param>
        /// <param name="sortOrderIndex">The order to sort by.</param>
        /// <param name="isGroupBy">Value indicating whether the sorted field is a grouped field.</param>
        public void Add(string propertyName, SortOrderType sortOrder, int sortOrderIndex, bool isGroupBy)
        {
            Add(new SortedField(propertyName, sortOrder, sortOrderIndex, isGroupBy));
        }

        /// <summary>
        /// Adds the specified <see cref="T:CoreVelocity.DataAccess.SortedField"/> instance.
        /// </summary>
        /// <param name="SortedField">The <see cref="T:CoreVelocity.DataAccess.SortedField"/> instance to add to the collection.</param>
        public void Add(SortedField sortedField)
        {
            _innerList.Add(sortedField);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" /> is read-only. </exception>
        public void Clear()
        {
            _innerList.Clear();
        }

        /// <summary>
        /// Determines whether the SortedField collection contains the specified <see cref="T:CoreVelocity.DataAccess.SortedField"/>.
        /// </summary>
        /// <param name="sortedField">The <see cref="T:CoreVelocity.DataAccess.SortedField"/> to check the collection for.</param>
        /// <returns>true if the <see cref="T:CoreVelocity.DataAccess.SortedField"/> or not.</returns>
        public bool Contains(SortedField sortedField)
        {
            return (_innerList.Where(item => sortedField.PropertyName.ToUpper() == item.PropertyName.ToUpper()).FirstOrDefault() != null);
        }

        /// <summary>
        /// Copies the <see cref="T:CoreVelocity.DataAccess.SortedField"/> array to the collection.
        /// </summary>
        /// <param name="array">The array of <see cref="T:CoreVelocity.DataAccess.SortedField"/> instances to copy to the collection.</param>
        /// <param name="arrayIndex">Index of the <see cref="T:CoreVelocity.DataAccess.SortedField"/> in the array to copy to the collection.</param>
        public void CopyTo(SortedField[] array, int arrayIndex)
        {
            List<SortedField> tempList = new List<SortedField>();
            tempList.AddRange(_innerList);
            tempList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </returns>
        /// <value>Number of elements contained in the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.</value>
        public int Count
        {
            get
            {
                return _innerList.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />
        /// is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />
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
        /// Removes the specified <see cref="T:CoreVelocity.DataAccess.SortedField"/> instance from the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </summary>
        /// <param name="item">The <see cref="T:CoreVelocity.DataAccess.SortedField"/> to remove.</param>
        /// <returns>true if the <see cref="T:CoreVelocity.DataAccess.SortedField"/> instance was removed; otherwise, false.</returns>
        public bool Remove(SortedField item)
        {
            SortedField sortedField = _innerList.Where(c => c.PropertyName.ToUpper() == item.PropertyName.ToUpper()).FirstOrDefault();

            if (sortedField != null)
            {
                if (_innerList.Remove(sortedField))
                {
                    return true;
                }

            }
            return false;
        }

        #endregion

        #region IEnumerable<SortedField> Members

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used 
        /// to iterate through the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </returns>
        public IEnumerator<SortedField> GetEnumerator()
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
        /// used to iterate through the <see cref="T:CoreVelocity.DataAccess.SortedFieldCollection" />.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
