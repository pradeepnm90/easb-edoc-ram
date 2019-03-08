using Markel.Pricing.Service.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Traits
{
    public abstract class BaseSearchCriteria
    {
        private const int DEFAULT_PAGE_SIZE = 10;
        protected const char SORT_PARAMETER_SEPARATOR = ',';
        protected const string DESCENDING_ORDER_PREFIX = "-";

        // Paging
        public int? offset { set; get; }
        public int? limit { set; get; }
        // Sorting
        public string sort { set; get; }
        public string exportType { set; get; }

        protected int PageSize
        {
            get
            {
                int pageSize = (limit == null || limit == 0) ? DEFAULT_PAGE_SIZE : (int)limit;
                return pageSize;
            }
        }

        protected int PageIndex
        {
            get
            {
                int pageIndex = (offset != null) ? (int)offset : 0;
                return (pageIndex / PageSize);
            }
        }

        protected List<SortField> ToSortedFieldCollection()
        {
            List<SortField> sortedFields = new List<SortField>();
            if (sort != null)
            {
                string[] values = sort.Split(SORT_PARAMETER_SEPARATOR).Select(sValue => sValue.Trim()).ToArray();
                foreach (var item in values)
                {
                    //Added to avoid empty strings
                    if (!string.IsNullOrEmpty(item))
                    {
                        SortField sortedField = item.StartsWith(DESCENDING_ORDER_PREFIX) ? new SortField() { FieldName = SetSortedFieldName(item.Substring(1)), SortOrder = SortOrder.Descending.ToString() }
                        : new SortField() { FieldName = SetSortedFieldName(item), SortOrder = SortOrder.Ascending.ToString() };

                        sortedFields.Add(sortedField);
                    }
                }
            }
            return sortedFields;
        }

        protected virtual Dictionary<string, string> PropertyMappingCollection { get; }

        private string SetSortedFieldName(string fieldName)
        {
            if (PropertyMappingCollection != null && PropertyMappingCollection.Count > 0 && PropertyMappingCollection.ContainsKey(fieldName))
                return PropertyMappingCollection[fieldName];

            return Char.ToUpperInvariant(fieldName[0]) + fieldName.Substring(1);
        }

    }
}