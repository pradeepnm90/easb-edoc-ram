using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class EntityLockRepository : GenericRepository<ERMSDbContext, grs_PrLckGetLockedItem, int>, IEntityLockRepository
    {
		private int Lock_Category_Deals_Wait_Seconds = 60*3;
        public EntityLockRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public bool LockEntity(int categoryId, int entityId, int userId, out string errorMessage)
        {
			DateTime expirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(Lock_Category_Deals_Wait_Seconds));
            int result = Context.SpLckLockItem(categoryId, entityId, expirationTime, userId, true, "GRS", out errorMessage);
            return string.IsNullOrEmpty(errorMessage);
        }      

        public void UnlockEntity(int categotyId, int entityId, int userId)
        {
            int result = Context.SpLckUnLockItem(categotyId, entityId, userId);           
        }

        public IList<grs_PrLckGetLockedItem> GetEntityLocks(int categoryId, int entityId, int userId)
        {
			return Context.grs_PrLckGetLockedItem(categoryId, entityId, userId);
        }
    }


}
