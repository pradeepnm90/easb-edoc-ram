using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class TblClausesDealRepository : GenericRepository<ERMSDbContext, TbClaus, int>, ITblClausesDealRepository
    {
        public TblClausesDealRepository(IUserManager userManager, IAmbientDbContextLocator context) : base(userManager, context) { }

    }
}
