using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Enums;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class DealStatusSummariesRepository : GenericRepository<ERMSDbContext, grs_PrGetGrsDealCountByStatus, int>, IDealStatusSummariesRepository
    {
        public DealStatusSummariesRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public IList<grs_PrGetGrsDealCountByStatus> GetAllDealStatusSummaries(string exposures, string personids)
        {
            return Context.grs_PrGetGrsDealCountByStatus(exposures, personids);
        }
        public IList<grs_VExposureTreeExt> GetGlobalReExposureTree()
        {
            try
            {
                var tree = Context.grs_VExposureTreeExts.AsNoTracking().ToList();
                return tree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidPerson(int personId)
        {
            var id = Context.TbPersons.Where(p => p.PersonId == personId).Select(p => p.PersonId).ToList();
            return id.Count > 0;
        }

    }
}
