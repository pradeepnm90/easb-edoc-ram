using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Represents a lookup criteria when querying a data source.
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// Gets or sets the parameters to use for the critiera specified.
        /// </summary>
        CriteriaParameterCollection Parameters { get; set; }

        bool IsPaginationEnabled { get; set; }

        /// <summary>
        /// Gets or sets the pagination settings to apply to the criteria specified.
        /// </summary>
        IPagination Pagination { get; set; }

        /// <summary>
        /// Gets or sets the fields to sort by.
        /// </summary>
        SortedFieldCollection SortedFields { get; set; }

        /// <summary>
        /// Gets or sets the top number of records to retrieve from the query. IE: "SELECT TOP n FROM...."
        /// </summary>
        int TopNumberOfRecords { get; set; }

        List<int> GetIDListParam(string parameterName);
        List<string> GetStringListParam(string parameterName);
        int? GetIntParam(string parameterName);
        string GetStringParam(string parameterName);
        bool? GetBooleanParam(string parameterName);
        DateTime? GetDateTimeParam(string parameterName);

        double? GetDoubleParam(string parameterName);

    }
}
