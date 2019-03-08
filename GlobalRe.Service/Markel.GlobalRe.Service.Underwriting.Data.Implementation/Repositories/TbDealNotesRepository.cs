using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Linq;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class TbDealNotesRepository : GenericRepository<ERMSDbContext, TbDealnote, int>, ITbDealNotesRepository
    {
        public TbDealNotesRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public int GetNextNoteNumber()
        {
            return Context.TbDealnotes.Max(d => d.Notenum + 1); 
        }
    }

}
