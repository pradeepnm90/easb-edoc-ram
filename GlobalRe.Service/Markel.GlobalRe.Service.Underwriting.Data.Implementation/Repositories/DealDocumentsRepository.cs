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
    public class DealDocumentsRepository : GenericRepository<ERMSDbContext, grs_VKeyDocument, int>, IDealDocumentsRepository
    {
        public IList<grs_VKeyDocument> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure)
        {

            
            int dealnumber = !filenumber.IsNullOrEmpty() ? int.Parse(filenumber) : 0;
            int producerid = !producer.IsNullOrEmpty() ? int.Parse(producer) : GetUnderwritingTeam(Int32.Parse(filenumber));

            if (!isDocTypeStructure) // Loading default schema 
                return Context.grs_VKeyDocuments.AsNoTracking().Where(d => d.Filenumber == 0 && (d.Producer == 0 || d.Producer == producerid)).OrderBy(p => p.Sortorder).ToList<grs_VKeyDocument>();
            else                    // loading default schema and tagged key documents
                return Context.grs_VKeyDocuments.AsNoTracking().Where(d => (d.Filenumber == dealnumber || d.Filenumber == 0) && (d.Producer == 0 || d.Producer == producerid)).OrderBy(p => p.Sortorder).ToList<grs_VKeyDocument>();
        }

        public string GetFileType(int filenumber)
        {
          
            string sFileType = string.Empty;
           // Doctype is always 0 while fetching records from DMS.And it is non zero when adding actual document to DMS.

            //DMSSystem is 2 which is IR

          List < grs_VDmsFileType> lstFileType=  Context.grs_VDmsFileTypes.Where(d => (d.Dealnum == filenumber && d.DocType == 0 && d.DmsSystem == 2 )).ToList<grs_VDmsFileType>();
            sFileType = lstFileType[0].FileType;

            return sFileType;
        }
        public Boolean IsDealExists(int filenumber)
        {
            return Context.grs_VGrsDeals.Where(d => d.Dealnum == filenumber).ToList<grs_VGrsDeal>().Count > 0 ? true : false;
        }


        private int GetUnderwritingTeam(int filenumber)
        {
            return Int32.Parse(Context.grs_VGrsDeals.Where(d => d.Dealnum == filenumber).FirstOrDefault<grs_VGrsDeal>().Team.ToString());
        }


        public DealDocumentsRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            filenumber,
            producer
        }

        private enum SortParameters
        {
            filenumber,
            sortorder
        }

        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VKeyDocument> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VKeyDocuments;

            var predicate = PredicateBuilder.New<grs_VKeyDocument>(true);

            predicate = predicate.AndIf(FilterDocuments(searchCriteria.Parameters));

            PaginatedList<grs_VKeyDocument> pagingatedResult = new PaginatedList<grs_VKeyDocument>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "filenumber");
            return pagingatedResult;
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VKeyDocument, bool>> FilterDocuments(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.filenumber);
            if (id.HasValue)
                return s => s.Filenumber == id.Value;
            return null;
        }
    }
}
