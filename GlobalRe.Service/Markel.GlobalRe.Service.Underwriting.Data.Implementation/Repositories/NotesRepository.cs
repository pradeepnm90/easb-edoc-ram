
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
    public class NotesRepository : GenericRepository<ERMSDbContext, grs_VGrsNote, int>, INotesRepository
    {
        public NotesRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            Dealnum,
            notenumber
        }

        private enum SortParameters
        {
            NoteDate
        }

        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VGrsNote> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VGrsNotes;

            var predicate = PredicateBuilder.New<grs_VGrsNote>(true);

            predicate = predicate.AndIf(FilterDealNum(searchCriteria.Parameters));

            PaginatedList<grs_VGrsNote> pagingatedResult = new PaginatedList<grs_VGrsNote>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "Dealnum");
            return pagingatedResult;
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VGrsNote, bool>> FilterDealNum(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.Dealnum);
            if (id.HasValue)
                return s => s.Dealnum == id.Value;
            return null;
        }

        private Expression<Func<grs_VGrsNote, bool>> FilterNoteNum(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.notenumber);
            if (id.HasValue)
                return s => s.Dealnum == id.Value;
            return null;
        }

        public IList<grs_VGrsNote> GetNotes(int dealnumber)
        {
            IList < grs_VGrsNote > dealNotedata= Context.grs_VGrsNotes.Where(d => d.Dealnum == dealnumber).OrderByDescending(p => p.Notedate).ToList<grs_VGrsNote>();
            return dealNotedata;
           // return Context.grs_VGrsDealNotes.Where(d => d.Dealnum == dealnumber).OrderByDescending(p=>p.Notedate).ToList<grs_VGrsNote>();
        }

        public IList<grs_VGrsNote> GetNotebyNoteNumber(int notenumber)
        {
            IList<grs_VGrsNote> notedata = Context.grs_VGrsNotes.Where(d => d.Notenum == notenumber).ToList<grs_VGrsNote>();
            return notedata;
        }

        //public IList<grs_VGrsNote> GetNotes(SearchCriteria searchCriteria)
        //{
        //   // int personId = this._userManager.UserIdentity.NameId;
        //    var predicate = PredicateBuilder.New<grs_VGrsNote>(true);
        //    predicate = predicate.AndIf(FilterDealNum(searchCriteria.Parameters));
        //    Expression<Func<grs_VGrsNote, bool>> expression = FilterDealNum(searchCriteria.Parameters);
        //    if (expression != null)
        //        predicate = predicate.AndIf(expression);
        //    return Context.grs_VGrsDealNotes.Where(predicate).OrderByDescending(p => p.Dealnum).ToList();
        //}

    }
}
