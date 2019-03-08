
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
    public class CedantRepository : GenericRepository<ERMSDbContext, grs_VGrsCedant, int>, ICedantRepository
    {

        public IList<grs_VGrsCedant> GetCedants(string cedants, string cedantsparentgroup, string cedantsid, string cedantsparentgroupid, string cedantslocationid)
        {
            // Converting optional filters to type defined 

            int CedentID = 0, ParentGroupID = 0, LocationID = 0;
            if (!cedantsid.IsNullOrEmpty()) CedentID = int.Parse(cedantsid);
            if (!cedantsparentgroupid.IsNullOrEmpty()) ParentGroupID = int.Parse(cedantsparentgroupid);
            if (!cedantslocationid.IsNullOrEmpty()) LocationID = int.Parse(cedantslocationid);

            // Applying filters and setting sort order 

            if (!cedantsid.IsNullOrEmpty() &&                   // Optional search (addressing future requirements) search by id's cedant , group and location 
                !cedantsparentgroupid.IsNullOrEmpty() && !cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Cedantid == CedentID &&
                                                        d.Cedantgroupid == ParentGroupID &&
                                                        d.Locationid == LocationID).OrderByDescending(p => p.Cedantid).ToList<grs_VGrsCedant>();
            }
            else if (!cedantsid.IsNullOrEmpty() &&
                    !cedantsparentgroupid.IsNullOrEmpty() && cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Cedantid == CedentID &&
                                                        d.Cedantgroupid == ParentGroupID).OrderByDescending(p => p.Cedantid).ToList<grs_VGrsCedant>();
            }
            else if (!cedantsid.IsNullOrEmpty() &&
                    cedantsparentgroupid.IsNullOrEmpty() && !cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Cedantid == CedentID &&
                                                        d.Locationid == LocationID).OrderByDescending(p => p.Cedantid).ToList<grs_VGrsCedant>();
            }
            else if (cedantsid.IsNullOrEmpty() &&
                    !cedantsparentgroupid.IsNullOrEmpty() && !cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Cedantgroupid == ParentGroupID &&
                                                        d.Locationid == LocationID).OrderByDescending(p => p.Locationid).ToList<grs_VGrsCedant>();
            }
            else if (!cedantsid.IsNullOrEmpty() &&              // Cedant ID optional 
                    cedantsparentgroupid.IsNullOrEmpty() && cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Cedantid == CedentID).OrderByDescending(p => p.Cedantid).ToList<grs_VGrsCedant>();
            }
            else if (cedantsid.IsNullOrEmpty() &&               // Cedant parent group id optional 
                    !cedantsparentgroupid.IsNullOrEmpty() && cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Cedantgroupid == ParentGroupID).OrderByDescending(p => p.Cedantgroupid).ToList<grs_VGrsCedant>();
            }
            else if (cedantsid.IsNullOrEmpty() &&               // Location ID search optional 
                    cedantsparentgroupid.IsNullOrEmpty() && !cedantslocationid.IsNullOrEmpty())
            {
                return Context.grs_VGrsCedants.Where(d => d.Locationid == LocationID).OrderByDescending(p => p.Locationid).ToList<grs_VGrsCedant>();
            }
            else if (!cedants.IsNullOrEmpty() &&                // Search by freeflow textbox and wildcards criteria
                    !cedantsparentgroup.IsNullOrEmpty())
            {
                if(cedants.StartsWith("%") && cedantsparentgroup.StartsWith("%"))
                    return Context.grs_VGrsCedants.Where(d => d.Cedant.Contains(cedants.Replace("%", "")) && d.Cedantgroupname.Contains(cedantsparentgroup.Replace("%", ""))).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
                else if (cedants.StartsWith("%") && !cedantsparentgroup.StartsWith("%"))
                    return Context.grs_VGrsCedants.Where(d => d.Cedant.Contains(cedants.Replace("%","")) && d.Cedantgroupname.StartsWith(cedantsparentgroup)).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
                else if (!cedants.StartsWith("%") && cedantsparentgroup.StartsWith("%"))
                    return Context.grs_VGrsCedants.Where(d => d.Cedant.StartsWith(cedants) && d.Cedantgroupname.Contains(cedantsparentgroup.Replace("%",""))).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
                else
                    return Context.grs_VGrsCedants.Where(d => d.Cedant.StartsWith(cedants) && d.Cedantgroupname.StartsWith(cedantsparentgroup)).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
            }
            else if (!cedants.IsNullOrEmpty() &&
                    cedantsparentgroup.IsNullOrEmpty())
            {
                if(cedants.StartsWith("%"))
                    return Context.grs_VGrsCedants.Where(d => d.Cedant.Contains(cedants.Replace("%",""))).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
                else
                    return Context.grs_VGrsCedants.Where(d => d.Cedant.StartsWith(cedants)).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
            }
            else if (cedants.IsNullOrEmpty() &&
                    !cedantsparentgroup.IsNullOrEmpty())
            {
                if(cedantsparentgroup.StartsWith("%"))
                    return Context.grs_VGrsCedants.Where(d => d.Cedantgroupname.Contains(cedantsparentgroup.Replace("%",""))).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
                else
                    return Context.grs_VGrsCedants.Where(d => d.Cedantgroupname.StartsWith(cedantsparentgroup)).OrderByDescending(p => p.Cedant).ToList<grs_VGrsCedant>();
            }

            return null;
        }

        public CedantRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            Cedants,
            Cedantgroupname,
            Cedantcategories
        }

        private enum SortParameters
        {
            Cedants
        }

        public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VGrsCedant> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VGrsCedants;

            var predicate = PredicateBuilder.New<grs_VGrsCedant>(true);

            predicate = predicate.AndIf(FilterCedants(searchCriteria.Parameters));

            PaginatedList<grs_VGrsCedant> pagingatedResult = new PaginatedList<grs_VGrsCedant>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "Cedants");
            return pagingatedResult;
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria searchCriteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VGrsCedant, bool>> FilterCedants(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.Cedants);
            if (id.HasValue)
                return s => s.Cedantid == id.Value;
            return null;
        }


    }
}
