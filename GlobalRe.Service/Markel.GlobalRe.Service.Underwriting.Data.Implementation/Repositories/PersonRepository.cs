using LinqKit;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Extensions;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class PersonRepository : GenericRepository<ERMSDbContext, TbPerson, int>, IPersonRepository
    {
        public PersonRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            PersonId
        }

        private enum SortParameters
        {
            PersonId
        }

        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

		public TbPerson GetPerson(int personId)
		{
			return Context.TbPersons.Where(p => p.PersonId == personId).FirstOrDefault();
		}
		public IPaginatedList<TbPerson> Search(SearchCriteria searchCriteria)
        {
            var data = Context.TbPersons;

            var predicate = PredicateBuilder.New<TbPerson>(true);
            predicate = predicate.AndIf(FilterPersonId(searchCriteria.Parameters));

            PaginatedList<TbPerson> pagingatedResult = new PaginatedList<TbPerson>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "PersonId");

            return pagingatedResult;
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<TbPerson, bool>> FilterPersonId(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.PersonId);
            if (id.HasValue)
                return s => s.PersonId == id.Value;
            return null;
        }
    }
}
