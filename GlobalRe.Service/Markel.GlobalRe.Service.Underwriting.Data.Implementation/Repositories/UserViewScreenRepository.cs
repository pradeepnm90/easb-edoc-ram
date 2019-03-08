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
    public class UserViewScreenRepository : GenericRepository<ERMSDbContext, grs_VGrsUserView, int>, IUserViewScreenRepository
    {
        public UserViewScreenRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public IList<string> GetFilterParameters()
        {
            throw new System.NotImplementedException();
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

        public IPaginatedList<grs_VGrsUserView> Search(SearchCriteria searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

    }

}


