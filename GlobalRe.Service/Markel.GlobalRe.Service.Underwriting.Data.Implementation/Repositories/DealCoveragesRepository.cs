
using LinqKit;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Extensions;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class DealCoveragesRepository : GenericRepository<ERMSDbContext, grs_VGrsDealCoverage, int>, IDealCoveragesRepository
    {
        public IList<grs_VGrsDealCoverage> GetDealCoverages(int dealNumber)
        {
            return Context.grs_VGrsDealCoverages.Where(d => d.Dealnum == dealNumber).OrderByDescending(p => p.CoverId).ToList<grs_VGrsDealCoverage>();
        }


        public DealCoveragesRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            Dealnum
        }

        private enum SortParameters
        {
            Dealnum
        }


        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VGrsDealCoverage> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VGrsDealCoverages;

            var predicate = PredicateBuilder.New<grs_VGrsDealCoverage>(true);

            predicate = predicate.AndIf(FilterDealNum(searchCriteria.Parameters));

            PaginatedList<grs_VGrsDealCoverage> pagingatedResult = new PaginatedList<grs_VGrsDealCoverage>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "Dealnum");
            return pagingatedResult;
        }

        private Expression<Func<grs_VGrsDealCoverage, bool>> FilterDealNum(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.Dealnum);
            if (id.HasValue)
                return s => s.Dealnum == id.Value;
            return null;
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria searchCriteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

    }
}
