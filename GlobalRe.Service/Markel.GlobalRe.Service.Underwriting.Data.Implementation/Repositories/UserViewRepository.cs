using System.Collections.Generic;
using System.Linq;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;
using System;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class UserViewRepository : GenericRepository<ERMSDbContext, grs_TblUserview, int>, IUserViewRepository
    {
        public UserViewRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public IList<string> GetFilterParameters()
        {
            throw new System.NotImplementedException();
        }

        public int GetNextSortOrder(int userId, string screenname)
        {
            return Context.grs_TblUserviews.Where(u => u.Userid == userId && u.Screenname == screenname).Select(u => u.Sortorder).DefaultIfEmpty().Max() + 1 ;
        }

        public IList<string> GetSortParameters()
        {
            throw new System.NotImplementedException();
        }

        //public void ResetDefaultOnUserViews(int userId, string screenName, int viewId)
        //{
        //    Context.grs_TblUserviews.Where(v => v.Userid == userId && v.Screenname == screenName && v.ViewId != viewId && v.Default)
        //        .ToList().ForEach(v => v.Default = false);
        //}

        public IPaginatedList<grs_TblUserview> Search(SearchCriteria searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

    }

}

