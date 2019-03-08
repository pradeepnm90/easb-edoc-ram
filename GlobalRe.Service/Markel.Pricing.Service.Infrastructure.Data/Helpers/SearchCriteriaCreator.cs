using Markel.Pricing.Service.Infrastructure.Data;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public static class SearchCriteriaCreator
    {
        public static Criteria ToCriteria(this SearchCriteria input)
        {
            Criteria searchCriteria = new Criteria();
            searchCriteria.Pagination.PageIndex = input.PageIndex - 1;
            searchCriteria.Pagination.PageSize = input.PageSize;

            foreach (var param in input.Parameters)
            {
                if (param.Value != null)
                {
                    searchCriteria.Parameters.Add(param.Name, param.Value);
                }

            }
            searchCriteria.SortedFields = new SortedFieldCollection();

            foreach (SortField sortField in input.SortedFields)
            {
                searchCriteria.SortedFields.Add(new SortedField(sortField.FieldName, sortField.SortOrder));
            }

            return searchCriteria;
        }
    }
}
