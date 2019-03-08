using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents a collection of <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/>.
    /// </summary>
    public sealed class CriteriaParameterCollection : ICollection<ICriteriaParameter>
    {
        #region Constants

        private const string IndexedParameterFormatString = "{0}{1}";

        #endregion

        #region Private Members

        /// <summary>
        /// Internal <seealso cref="System.Collections.Generic.IList`1"/> containing the collection of <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/> objects.
        /// </summary>
        private IList<ICriteriaParameter> _innerList;

        /// <summary>
        /// Internal <see cref="System.Collections.Generic.Dictionary`1`2"/>  containing a numerical index
        /// of paramters using the same name. This is to generate a unique numeric name for each parameter. 
        /// We don't generate a random name so the parameter names stay the same if running the query
        /// against SQL Server so the execution plans stay the same.
        /// </summary>
        private Dictionary<string, int> _innerParameterIndexList;
        
        #endregion 

        #region Constructor

        /// <summary>
        /// Default parameterless constructor for CriteriaParameterCollection
        /// </summary>
        public CriteriaParameterCollection()
        {
            _innerList = new List<ICriteriaParameter>();
            _innerParameterIndexList = new Dictionary<string, int>();
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Removes the ICriteriaParamters from the collection that have
        /// been automatically generated. This happens within the DbTable 
        /// so the calling source doesn't get the generated parameters back. If they used
        /// the same collection, they would be added a second time causing multiple parameters
        /// of the same name.
        /// </summary>
        public void RemoveGeneratedParameters()
        {
            this.ToList().Where(p => p.IsGenerated == true).ToList().ForEach(p => this.Remove(p));
        }

        #endregion

        #region ICollection<ICriteriaParameter> Members

        /// <summary>
        /// Gets the <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter" /> with the specified <see cref="CoreVelocity.DataAccess.ICriteriaParameter.ParameterName"/>.
        /// </summary>
        /// <value></value>
        public ICriteriaParameter this[string parameterName]
        {
            get
            {
                return _innerList.Where(item => item.ParameterName.ToUpper() == parameterName.ToUpper()).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets the <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter" /> with the specified index.
        /// </summary>
        /// <param name="index">Index of parameter in the collection.</param>
        /// <returns></returns>
        public ICriteriaParameter this[int index]
        {
            get
            {
                return _innerList[index];
            }
        }

        /// <summary>
        /// Adds a range of <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/> instances to the collection.
        /// </summary>
        /// <param name="critieriaParameters">The collection of <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/> instances to add to the collection.</param>
        public void AddRange(IEnumerable<ICriteriaParameter> critieriaParameters)
        {
            Asserter.AssertIsNotNull("critieriaParameters", critieriaParameters);
            foreach (ICriteriaParameter criteriaParameter in critieriaParameters)
            {
                criteriaParameter.ParameterName = getParameterName(criteriaParameter.ParameterName);
                Add(criteriaParameter);
            }
        }

        /// <summary>
        /// Creates a parameter and adds it to the collection.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public void Add(string parameterName, object value)
        {
            Asserter.AssertIsNotNull("parameterName", parameterName);
            Asserter.AssertIsNotNull("value", value);
            string propertyName = parameterName;
            parameterName = getParameterName(parameterName);
            Add(new CriteriaParameter(parameterName, value, propertyName));
        }
      
        /// <summary>
        /// Adds an <seealso cref="CoreVelocity.Core.Data.ICritieriaParameter"/> to the collection.
        /// </summary>
        /// <param name="criteriaParameter">The <seealso cref="CoreVelocity.Core.Data.ICritieriaParameter"/> to add to the collection.</param>
        public void Add(ICriteriaParameter criteriaParameter)
        {
            Asserter.AssertIsNotNull("criteriaParameter", criteriaParameter);
            criteriaParameter.ParameterName = getParameterName(criteriaParameter.ParameterName);
            _innerList.Add(criteriaParameter);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" /> is read-only. </exception>
        public void Clear()
        {
            _innerList.Clear();
        }

        /// <summary>
        /// Determines whether the CriteriaParameter collection contains the specified parameter
        /// </summary>
        /// <param name="criteriaParameter">The parameter to check the collection for</param>
        /// <returns>True if the ICriteriaParameter collection contains the parameter</returns>
        public bool Contains(ICriteriaParameter criteriaParameter)
        {
            Asserter.AssertIsNotNull("criteriaParameter", criteriaParameter);
            return (_innerList.Where(item => criteriaParameter.ParameterName.ToUpper() == item.ParameterName.ToUpper()).FirstOrDefault() != null);
        }

        public bool Contains(string parameterName)
        {
            Asserter.AssertIsNotNullOrEmptyString("parameterName", parameterName);
            return (this[parameterName] != null);
        }

        /// <summary>
        /// Copies the <see cref="T:CoreVelocity.DataAccess.SortedField"/> array to the collection.
        /// </summary>
        /// <param name="array">The array of <see cref="T:CoreVelocity.DataAccess.SortedField"/> instances to copy to the collection.</param>
        /// <param name="arrayIndex">Index of the <see cref="T:CoreVelocity.DataAccess.SortedField"/> in the array to copy to the collection.</param>
        public void CopyTo(ICriteriaParameter[] array, int arrayIndex)
        {
            Asserter.AssertIsNotNull("array", array);
            Asserter.AssertRange("arrayIndex", 0, array.Length, arrayIndex);

            if(arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }
            List<ICriteriaParameter> tempList = new List<ICriteriaParameter>();
            tempList.AddRange(_innerList);
            tempList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />.
        /// </returns>
        /// <value>Number of elements contained in the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />.</value>
        public int Count
        {
            get
            {
                return _innerList.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />
        /// is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />
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
        /// Removes the specified <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/> instance from the <see cref="T:CoreVelocity.DataAccess.CriteriaParameterCollection" />.
        /// </summary>
        /// <param name="item">The <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/> to remove.</param>
        /// <returns>true if the <see cref="T:CoreVelocity.DataAccess.ICriteriaParameter"/> instance was removed; otherwise, false.</returns>
        public bool Remove(ICriteriaParameter item)
        {
            Asserter.AssertIsNotNull("item", item);
            ICriteriaParameter critieriaParameter = _innerList.Where(c => c.ParameterName.ToUpper() == item.ParameterName.ToUpper()).FirstOrDefault();

            if (critieriaParameter != null)
            {
                if (_innerList.Remove(critieriaParameter))
                {
                    return true;
                }

            }
            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks to see if the specified parameter name is already in the collection, if 
        /// that's the case, we generated a new name with a number at the end, ie: ParameterName0, ParameterName1, ParameterName2.
        /// </summary>
        /// <param name="parameterName">The specified name of the parameter.</param>
        /// <returns>String containing the name of the parameter to use when adding it to the collection.</returns>
        private string getParameterName(string parameterName)
        {
            Asserter.AssertIsNotNullOrEmptyString("parameterName", parameterName);

            if (Contains(parameterName))
            {
                int parameterIndex = 0;
                if (_innerParameterIndexList.Count > 0 && _innerParameterIndexList.ContainsKey(parameterName))
                {
                    _innerParameterIndexList[parameterName]++;
                }
                else
                {
                    _innerParameterIndexList[parameterName] = 0;
                }
                parameterIndex = _innerParameterIndexList[parameterName];
                // Change the parameter's name to ParameterName + index (with the specified format string.)
                return string.Format(IndexedParameterFormatString, parameterName, parameterIndex);
            }
            return parameterName;
        }

        #endregion

        #region IEnumerable<SortedField> Members

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:CoreVelocity.DataAccess.CritieriaParameterCollection" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used 
        /// to iterate through the <see cref="T:CoreVelocity.DataAccess.CritieriaParameterCollection" />.
        /// </returns>
        public IEnumerator<ICriteriaParameter> GetEnumerator()
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
