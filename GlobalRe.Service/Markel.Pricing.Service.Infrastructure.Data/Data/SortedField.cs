using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Data.SqlClient;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents a sortable field mapped by propertyname and
    /// the sort order.
    /// </summary>
    public sealed class SortedField : IField
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedField"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="sortOrder">The sort order.</param>
        public SortedField(string propertyName, SortOrderType sortOrder)
            : this(propertyName, sortOrder, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedField"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="sortOrderIndex">Index of the sort order.</param>
        public SortedField(string propertyName, SortOrderType sortOrder, int sortOrderIndex)
            : this(propertyName, sortOrder, sortOrderIndex, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedField"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="sortOrderIndex">Index of the sort order.</param>
        /// <param name="isGroupBy">Indicates a grouped field if set to true </param>        
        public SortedField(string propertyName, SortOrderType sortOrder, int sortOrderIndex, bool isGroupBy)
        {
            PropertyName = propertyName;
            SortOrder = sortOrder;
            SortOrderIndex = sortOrderIndex;
            IsGroupBy = isGroupBy;
        }

        #endregion
        #region Public Properties

        /// <summary>
        /// Gets or sets the order to sort this field by.
        /// </summary>
        public int SortOrderIndex { get; set; }

        /// <summary>
        /// Gets or sets the property name to sort on.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the sort order to use when sorting this field.
        /// </summary>
        public SortOrderType SortOrder { get; set; }

        /// <summary>
        /// Gets or sets whether the sorted field is a grouped field, group
        /// by fields will come before any sorted fields.
        /// </summary>
        public bool IsGroupBy { get; set; }

        #endregion
       
    }
}
