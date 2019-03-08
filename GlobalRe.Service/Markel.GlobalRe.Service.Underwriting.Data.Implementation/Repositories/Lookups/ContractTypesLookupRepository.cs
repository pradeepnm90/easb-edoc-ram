
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
    public class ContractTypesLookupRepository : GenericRepository<ERMSDbContext, grs_VGrsContractType, int>, IContractTypesLookupRepository
    {
        public ContractTypesLookupRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }


        private enum FilterParameters
        {
            AssumedFlag
        }


        private enum SortParameters
        {
            Code
        }

        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VGrsContractType> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VGrsContractTypes;

            var predicate = PredicateBuilder.New<grs_VGrsContractType>(true);

            predicate = predicate.AndIf(FilterContractTypes(searchCriteria.Parameters));

            PaginatedList<grs_VGrsContractType> pagingatedResult = new PaginatedList<grs_VGrsContractType>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "AssumedFlag");
            return pagingatedResult;
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VGrsContractType, bool>> FilterContractTypes(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.AssumedFlag);
            if (id.HasValue)
                return s => s.Code == id.Value;
            return null;
        }

        public IList<grs_VGrsContractType> GetContractTypes()
        {
            IList<grs_VGrsContractType> contratTypedata;

            contratTypedata = Context.grs_VGrsContractTypes.AsNoTracking().Where(d => d.AssumedFlag == 1).OrderBy(p => p.Catorder).ToList<grs_VGrsContractType>();

            return contratTypedata;
        }


    }
}
