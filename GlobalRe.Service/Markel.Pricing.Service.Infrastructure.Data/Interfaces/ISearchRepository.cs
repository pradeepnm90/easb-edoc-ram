using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Data.Interfaces
{
    public interface ISearchRepository<CLASS, PRIMARY_KEY> : IGenericRepository<CLASS, PRIMARY_KEY>
        where CLASS : class
        where PRIMARY_KEY : IComparable
    {
        IPaginatedList<CLASS> Search(SearchCriteria searchCriteria);
        IEnumerable SearchDistinct(SearchCriteria searchCriteria, string distinctColumn);
        IList<string> GetFilterParameters();
        IList<string> GetSortParameters();

    }
}
