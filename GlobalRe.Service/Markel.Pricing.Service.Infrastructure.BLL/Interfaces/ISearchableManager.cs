using System.Collections;
using System.Collections.Generic;
using Markel.Pricing.Service.Infrastructure.Data;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    public interface ISearchableManager<CLASS> : IBaseManager where CLASS : IBusinessEntity
    {
        IPaginatedList<CLASS> Search(SearchCriteria criteria);
        IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn);
    }
}
