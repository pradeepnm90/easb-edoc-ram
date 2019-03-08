using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Markel.Pricing.Service.Infrastructure.Data.Helpers;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents a collection of objects that can be individually accessed by index and are
    /// paginated based on the page properties set on the collection.
    /// </summary>
    /// <typeparam name="CLASS">The type of elements in the paginated list. </typeparam>
    [DebuggerDisplay("Count = {Items.Count}")]
    [Serializable]
    [DataContract]
    public class PaginatedList<CLASS> : IPaginatedList<CLASS>
    {
        #region Public Properties

        private IList<CLASS> _items = null;

        /// <summary>
        /// Gets or sets a collection of objects.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DataMember]
        public IList<CLASS> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        [DataMember]
        public CLASS SummaryItem { get; set; } = (CLASS)Activator.CreateInstance(typeof(CLASS));

        #endregion

        #region Constants

        const string SYSTEM_NAMESPACE = "System";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList&lt;T&gt;"/> class.
        /// </summary>
        public PaginatedList()
        {
            _items = new List<CLASS>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public PaginatedList(IEnumerable<CLASS> items, SearchCriteria criteria, string defaultSortColumn) : this()
        {
            AddRange(items.AsQueryable(), criteria, defaultSortColumn);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a range of objects type of T to the collection.
        /// </summary>
        /// <param name="range">Source of objects to add to the collection.</param>
        public void AddRange(IEnumerable<CLASS> source)
        {
            source.ToList().ForEach(o => Items.Add(o));
        }

        /// <summary>
        /// Adds a range of objects type of T to the collection.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="defaultSortColumn">The default sort column.</param>
        public void AddRange(IQueryable<CLASS> query, SearchCriteria criteria, string defaultSortColumn)
        {
            PageIndex = 0;
            PageSize = 20;
            Items.Clear();
            List<object> expressionParams = null;
            string dynamicFilter = null;
            string dynamicOrderBy = null;
            string summarySelect = null;

            if (criteria != null)
            {
                PageSize = (criteria.PageSize <= 0) ? 20 : criteria.PageSize;
                PageIndex = (criteria.PageIndex < 0) ? 0 : criteria.PageIndex;

                // Initialize filter query filter and sort order
                dynamicFilter = criteria.Parameters.ToFilterExpressionString<CLASS>(ref expressionParams);
                dynamicOrderBy = criteria.SortedFields.ToSortExpressionString<CLASS>(defaultSortColumn);
                summarySelect = GetSummarySelectString(criteria.AggregateFields);
            }
            else
            {
                dynamicOrderBy = ((IReadOnlyCollection<SortField>)null).ToSortExpressionString<CLASS>(defaultSortColumn);
            }


            if (!string.IsNullOrWhiteSpace(dynamicFilter))
                query = query.Where(dynamicFilter, expressionParams.ToArray());

            if (!string.IsNullOrEmpty(summarySelect))
            {
                var result = query.GroupBy(x => 1).Select(summarySelect);

                foreach (var data in result)
                {
                    string[] str = data.ToString().Replace("{", "").Replace("}", "").Split(',');
                    foreach (string col in str)
                    {
                        string propertyName = col.Substring(0, col.IndexOf("=")).Trim();
                        string value = col.Substring(col.IndexOf("=") + 1).Trim();
                        if (propertyName.Equals("TotalRecordCount"))
                        {
                            TotalRecordCount = System.Convert.ToInt32(value);
                        }
                        else
                        {
                            SetValue(propertyName, value.TrimValue("0"));
                        }

                    }
                }

            }
            else
            {

                TotalRecordCount = query.Count();
            }
            //Total result count

            //If page number should be > 0 else set to first page
            if (TotalRecordCount <= PageSize || PageIndex <= 0) PageIndex = 0;

            //Calculate page count
            PageCount = (TotalRecordCount > 0 && PageSize > 0) ? (int)Math.Ceiling((double)TotalRecordCount / (double)PageSize) : 0;

            if (query != null)
            {
                _items = query.OrderBy(dynamicOrderBy).Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }
        }

        private void SetValue(string propertyName, string value)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            Type objectType = SummaryItem.GetType();
            var propType = objectType.GetProperty(propertyName, bindingFlags);
            if (propType != null)
            {
                SummaryItem.SetPropertyValue(propertyName, value);
            }
            else
            {
                foreach (var prop in objectType.GetProperties().Where(pr => pr.PropertyType.IsClass && pr.PropertyType != typeof(string)))
                {
                    propType = prop.PropertyType.GetProperty(propertyName, bindingFlags); 
                    if (propType != null)
                    {
                        Type propertyType = propType.PropertyType;
                        Type propInfoType = propertyType;
                        if (FilterUtil.IsNullableType(propertyType))
                        {
                            propInfoType = propertyType.GetGenericArguments()[0];
                        }

                        object target = prop.GetValue(SummaryItem);
                        if (target == null)
                        {
                            target = Activator.CreateInstance(prop.PropertyType);
                            prop.SetValue(SummaryItem, target);
                        }
                        propType.SetValue(target, System.Convert.ChangeType(value, propInfoType));
                    }
                }
            }
        }

        private string GetSummarySelectString(IEnumerable<AggregateField> aggregateFields)
        {
            if (aggregateFields.Count() <= 0)
                return null;
            string summaryString = string.Empty;

            foreach (var item in aggregateFields)
            {
                var fields = item.Field.Split('.');
                var field = fields[fields.Length - 1];
                summaryString += string.Format("{0}(it.{1}) as {2},", item.AggregateFunction, item.Field, field);
            }
            //for (int i = 0; i < aggregateFields.Count(); i++)
            //{
            //    var fields = summaryFields.ToArray()[i].Split('.');
            //    var field = fields[fields.Length - 1];
            //    summaryString += string.Format("Sum(it.{0}) as {1},", summaryFields.ToArray()[i], field);
            //}
            string sumExpression = string.Format("new ( {0} Count() as TotalRecordCount)", summaryString);
            //var t = data.Select(sumExpr);
            return sumExpression;
        }

        protected string GetDynamicValue(dynamic entity, string propertyName)
        {
            string value = string.Empty;
            foreach (var item in entity.Children())
            {
                if (string.Equals(item.Name.ToString().Trim(), propertyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (item.Value != null)
                        value = System.Convert.ToString(item.Value);
                }
            }
            return value;
        }

        public IPaginatedList<T> ConvertTo<T>() where T: class
        {
            return new PaginatedList<T>()
            {
                Items = Items.Select(i => i as T).ToList(),
                SummaryItem = SummaryItem as T,
                PageSize = PageSize,
                PageIndex = PageIndex,
                PageCount = PageCount,
                TotalRecordCount = TotalRecordCount,
                HasPreviousPage = HasPreviousPage,
                HasNextPage = HasNextPage
            };
        }

        #endregion

        #region IPaginatedList<T> Members

        /// <summary>
        /// Gets or sets the number of records per page to return.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the current page index the record set is on.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets the current page number. (PageIndex + 1)
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageNumber
        {
            get
            {
                return PageIndex + 1;
            }
        }

        /// <summary>
        /// Gets or sets the total number of pages in the recordset.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of records in the recordset.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// Gets whether the paginated list has a previous page or not.
        /// </summary>
        [XmlElement]
        [DataMember]
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
            set { }
        }

        /// <summary>
        /// Gets whether the paginated list has a next page or not.
        /// </summary>
        [XmlElement]
        [DataMember]
        public bool HasNextPage
        {
            get
            {
                return ((PageIndex != int.MaxValue) && (PageIndex + 1 < PageCount));
            }
            set { }
        }


        #endregion
    }
}
