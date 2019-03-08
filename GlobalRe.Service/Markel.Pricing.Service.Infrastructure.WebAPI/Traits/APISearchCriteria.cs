using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Traits
{
    public class APISearchCriteria : APIPaginationCriteria
    {
        #region Properties

        public List<SortField> SortedFields { set; get; }

        public List<string> GroupByFields { set; get; }

        public List<string> Columns { set; get; }

        public List<AggregateField> AggregateFields { set; get; }

        public List<FilterParameter> FilterParameters { get; set; }

        public bool IsDistinct { get; set; }

        public ExportTypeEnum? ExportType { set; get; }

        #endregion Properties

        #region Virtual Methods

        /// <summary>
        /// Override this method to make any modifications to the filter params.
        /// </summary>
        /// <param name="filterParams">The filter parameters.</param>
        protected virtual void OnUpdateFilterParams(List<FilterParameter> filterParams) { }

        #endregion Virtual Methods

        #region Methods

        public void Add(FilterParameter filterParameter)
        {
            FilterParameters = (FilterParameters != null) ? FilterParameters : new List<FilterParameter>();
            FilterParameters.Add(filterParameter);
        }

        public SearchCriteria ToSearchCriteria()
        {
            OnUpdateFilterParams(FilterParameters = FilterParameters ?? new List<FilterParameter>());

            return new SearchCriteria(
                sortedFields: SortedFields,
                groupByFields: GroupByFields,
                aggregateFields: AggregateFields,
                pageIndex: pageIndex,
                pageSize: pageSize,
                parameters: FilterParameters?.Where(p => !string.IsNullOrWhiteSpace(p.Name)).ToList()
            );
        }

        #endregion Methods
    }
}