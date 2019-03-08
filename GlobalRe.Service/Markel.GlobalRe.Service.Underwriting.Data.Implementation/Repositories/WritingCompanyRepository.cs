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
using System.Linq.Dynamic;
using EntityFramework.DbContextScope.Interfaces;


namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class WritingCompanyRepository : GenericRepository<ERMSDbContext, grs_VPaperExt, int>, IWritingCompanyRepository
    {
        public IList<grs_VPaperExt> GetWritingCompany()
        {
            
            return Context.grs_VPaperExts.Where(d => d.Active == 1).OrderBy(p => p.AssumedSortOrder).ToList<grs_VPaperExt>();
        }


        public IList<grs_VPaperExt> GetWritingCompany(Boolean isGRSFlag)
        {
             return Context.grs_VPaperExts.Where(d => d.Active == 1 && d.AssumedFlag == 1).OrderBy(p => p.AssumedSortOrder).ToList<grs_VPaperExt>();//d.IsGrsDisplay == 1 TO CHECK
            
        }

        public WritingCompanyRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            Active,
            Papernum,
            Companyname
        }

        private enum SortParameters
        {
            Active,
            Companyname,
            IsGrsDisplay
        }

        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VPaperExt> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VPaperExts;

            var predicate = PredicateBuilder.New<grs_VPaperExt>(true);

            predicate = predicate.AndIf(FilterWritingCompanyNum(searchCriteria.Parameters));

            PaginatedList<grs_VPaperExt> pagingatedResult = new PaginatedList<grs_VPaperExt>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "companyName");
            return pagingatedResult;
        }

    public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
    {
        throw new NotImplementedException();
    }

    private Expression<Func<grs_VPaperExt, bool>> FilterWritingCompanyNum(IEnumerable<FilterParameter> parameters)
    {
        int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.Papernum);
        if (id.HasValue)
                return s => s.Papernum == id.Value;
            return null;
        }


    }
}
