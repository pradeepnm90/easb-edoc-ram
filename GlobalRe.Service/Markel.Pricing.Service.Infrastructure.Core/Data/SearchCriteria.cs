using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    public class SearchCriteria
    {
        #region Private Variables

        private List<FilterParameter> _parameters = new List<FilterParameter>();
        private List<SortField> _sortedFields = new List<SortField>();
        private List<string> _groupByFields = new List<string>();
        private List<AggregateField> _aggregateFields = new List<AggregateField>();

        #endregion Private Variables

        #region Properties

        public IReadOnlyCollection<FilterParameter> Parameters { get { return _parameters.AsReadOnly(); } }
        public IReadOnlyCollection<SortField> SortedFields { get { return _sortedFields.AsReadOnly(); } }
        public IReadOnlyCollection<string> GroupByFields { get { return _groupByFields.AsReadOnly(); } }
        public IReadOnlyCollection<AggregateField> AggregateFields { get { return _aggregateFields.AsReadOnly(); } }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public string DistinctField { get; private set; }

        #endregion Properties

        #region Constructors

        public SearchCriteria() { }

        public SearchCriteria(IList<FilterParameter> parameters, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;

            AddRange(parameters);
        }

        public SearchCriteria(IList<FilterParameter> parameters, IList<SortField> sortedFields, IList<string> groupByFields, int pageIndex, int pageSize, string distinctField = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            DistinctField = distinctField;

            AddRange(parameters);
            AddRange(sortedFields);
            AddRange(groupByFields);
        }

        public SearchCriteria(IList<FilterParameter> parameters, IList<SortField> sortedFields, IList<string> groupByFields, IList<AggregateField> aggregateFields, int pageIndex, int pageSize, string distinctField = null)
            :this(parameters,sortedFields,groupByFields,pageIndex,pageSize,distinctField)
        {
            AddRangeAggregateField(aggregateFields);
        }

        #endregion Constructors

        #region Methods

        public void Add(FilterParameter parameter)
        {
            if (parameter != null)
                _parameters.Add(parameter);
        }

        public void AddRange(IList<FilterParameter> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                _parameters.AddRange(parameters);
        }

        public void Add(SortField sortedField)
        {
            if (sortedField != null)
                _sortedFields.Add(sortedField);
        }

        public void AddRange(IList<SortField> sortedFields)
        {
            if (sortedFields != null && sortedFields.Count > 0)
                _sortedFields.AddRange(sortedFields);
        }
       
        public void Add(string groupByField)
        {
            if (groupByField != null)
                _groupByFields.Add(groupByField);
        }

        public void AddRange(IList<string> groupByFields)
        {
            if (groupByFields != null && groupByFields.Count > 0)
                _groupByFields.AddRange(groupByFields);
        }

        public void Remove(IList<string> paramNames)
        {
            if (paramNames != null)
            {
                foreach (var paramName in paramNames)
                {
                    Remove(paramName);
                }
            }
        }

        public void AddRangeAggregateField(IList<AggregateField> fields)
        {
            if (fields != null && fields.Count > 0)
                _aggregateFields.AddRange(fields);
        }

        public void Remove(string paramName)
        {
            if (!string.IsNullOrEmpty(paramName))
            {
                FilterParameter item;
                while ((item = _parameters.FirstOrDefault(paramName)) != null)
                {
                    _parameters.Remove(item);
                }
            }
        }

        public SearchCriteria MapProperties<F, T>()
        {
            _parameters.ForEach(p => { p.Name = PropertyMapper.GetPropertyName<F, T>(p.Name); });
            _sortedFields.ForEach(s => { s.FieldName = PropertyMapper.GetPropertyName<F, T>(s.FieldName); });
            _aggregateFields.ForEach(p => { p.Field = PropertyMapper.GetPropertyName<F, T>(p.Field); });

            //IEnumerable<string> sumFields = this._summaryFields.Select((field) => PropertyMapper.GetPropertyName<F, T>(field)).ToList();
            //_summaryFields.Clear();
            //_summaryFields.AddRange(sumFields);

            // Validate that mapped properties are valid
            var validProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();
            validProperties.AddRange(PropertyMapper.GetReferenceOnlyProperties<F, T>());
            ValidateSearchCriteria(validProperties, validProperties);

            return this;
        }

        /// <summary>
        /// Validates filter and sort parameters in search criteria are falid. Throws IllegalAPIArgumentException if fields do not match.
        /// </summary>
        /// <param name="validFilterParameters"></param>
        /// <param name="validSortParameters"></param>
        public void ValidateSearchCriteria(IList<string> validFilterParameters, IList<string> validSortParameters)
        {
            IllegalArgumentAPIException exception = new IllegalArgumentAPIException("Invalid Search Criteria");

            // Filter Parameters
            //foreach (Parameter parameter in this.Parameters)
            //{
            //    if (!validFilterParameters.Contains(parameter.Name) && !parameter.Name.Contains("."))
            //    {
            //        exception.Add(parameter.Name, "Unknown Search Parameter");
            //    }
            //}

            //foreach (SortField field in this.SortedFields)
            //{
            //    //TODO - PS : Remove this check 
            //    if (validSortParameters.Count > 0)
            //    {
            //        if (!validSortParameters.Contains(field.FieldName) && !field.FieldName.Contains("."))
            //        {
            //            exception.Add(field.FieldName, "Unknown Sort Field");
            //        }
            //    }
            //}

            //foreach (GroupField field in this.GroupByFields)
            //{
            //    //TODO - PS : Remove this check 
            //    if (validSortParameters.Count > 0)
            //    {
            //        if (!validSortParameters.Contains(field.FieldName) && !field.FieldName.Contains("."))
            //        {
            //            exception.Add(field.FieldName, "Unknown Sort Field");
            //        }
            //    }
            //}

            // Throw Exception if errors exist
            if (exception.Details.Count > 0)
            {
                throw exception;
            }
        }

        public string GetStringValue(string parameterName)
        {
            return Parameters.FirstOrDefault(parameterName) != null ? Parameters.FirstOrDefault(parameterName).Value.ToString().Trim() : string.Empty;
        }

        public DateTime? GetDateTimeValue(string parameterName)
        {
            if (Parameters.FirstOrDefault(parameterName) == null) return null;

            DateTime date;
            if (DateTime.TryParse(Parameters.FirstOrDefault(parameterName).Value.ToString(), out date))
            {
                return date;
            }
            return null;
        }

        public bool ContainsAnyParameter(params Enum[] parameters)
        {
            return ContainsAnyParameter(parameters.Select(p => p.ToString()).ToArray());
        }

        public bool ContainsAnyParameter(params string[] parameterNames)
        {
            foreach (string parameterName in parameterNames)
            {
                if (Parameters.FirstOrDefault(parameterName) != null) return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("'PageIndex': '{0}', 'PageSize': '{1}'", PageIndex, PageSize);
        }

        #endregion Methods
    }
}
