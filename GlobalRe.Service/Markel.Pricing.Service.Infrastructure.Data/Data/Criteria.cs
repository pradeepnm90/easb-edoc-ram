using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents a criteria to be used for passing parameters between tiers or facades.
    /// </summary>
    [Serializable]
    public class Criteria : ICriteria
    {
        #region Private Members

        /// <summary>
        /// Backing field for CriteriaParameter.
        /// </summary>
        [NonSerialized]
        private CriteriaParameterCollection _criteriaParameters;

        /// <summary>
        /// Backing field for SortedFields.
        /// </summary>
        [NonSerialized]
        private SortedFieldCollection _sortedFields;

        /// <summary>
        /// Backing field for Pagination.
        /// </summary>
        private IPagination _pagination;

        #endregion

        #region ICriteria Members

        /// <summary>
        /// Gets or sets the list of parameters to use in the update.
        /// </summary>
        public CriteriaParameterCollection Parameters
        {
            get
            {
                if (_criteriaParameters == null)
                {
                    _criteriaParameters = new CriteriaParameterCollection();
                }
                return _criteriaParameters;
            }
            set
            {
                _criteriaParameters = value;
            }
        }

        [DefaultValue(false)]
        public bool IsPaginationEnabled { get; set; }

        /// <summary>
        /// Gets or sets a collection of fields to sort by.
        /// </summary>
        public SortedFieldCollection SortedFields
        {
            get
            {
                if (_sortedFields == null)
                {
                    _sortedFields = new SortedFieldCollection();
                }
                return _sortedFields;
            }
            set
            {
                _sortedFields = value;
            }
        }

        /// <summary>
        /// Gets or sets the pagination settings to use for the query.
        /// </summary>
        public IPagination Pagination
        {
            get
            {
                if (_pagination == null)
                {
                    _pagination = new Pagination();
                }
                return _pagination;
            }
            set
            {
                _pagination = value;
            }
        }

        /// <summary>
        /// Gets or sets the top number of records to retrieve from the query. IE: "SELECT TOP n FROM...."
        /// </summary>
        public int TopNumberOfRecords { get; set; }

        #endregion

        public List<int> GetIDListParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
            {
                List<int> idList = Parameters[parameterName].Value.ToString().Split(',').Select(x => Int32.Parse(x)).ToList();
                return idList;
            }
            return null;
        }

        public List<string> GetStringListParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
            {
                List<string> idList = Parameters[parameterName].Value.ToString().Split(',').Select(x => x).ToList();
                return idList;
            }
            return null;
        }

        public int? GetIntParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && Parameters[parameterName].Value != null && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
            {
                int result = 0;
                if (Int32.TryParse(Parameters[parameterName].Value.ToString(), out result))
                    return result;
            }
           
            return null;
        }

        public string GetStringParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && Parameters[parameterName].Value != null && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
                return Parameters[parameterName].Value.ToString();
            return string.Empty;
        }

        public bool? GetBooleanParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && Parameters[parameterName].Value != null && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
                return Boolean.Parse(Parameters[parameterName].Value.ToString());
            return null;
        }

        public DateTime? GetDateTimeParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && Parameters[parameterName].Value != null && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
                return DateTime.Parse(Parameters[parameterName].Value.ToString());
            return null;
        }


        public double? GetDoubleParam(string parameterName)
        {
            if (Parameters.Contains(parameterName) && Parameters[parameterName].Value != null && !string.IsNullOrEmpty(Parameters[parameterName].Value.ToString()))
                return Double.Parse(Parameters[parameterName].Value.ToString());
            return null;
        }
    }
}

