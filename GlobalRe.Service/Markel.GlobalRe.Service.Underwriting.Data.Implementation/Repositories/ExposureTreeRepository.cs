using System;
using System.Collections.Generic;
using System.Linq;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class ExposureTreeRepository : GenericRepository<ERMSDbContext, grs_VExposureTreeExt, int>, IExposureTreeRepository
    {
        public ExposureTreeRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public IList<grs_VExposureTreeExt> GetGlobalReExposureTree()
        {
            try
            {
                var tree = Context.grs_VExposureTreeExts.AsNoTracking().ToList();
                if (tree == null) { throw new NotFoundAPIException($"Exposure Tree does not defined in database."); }
                return tree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

