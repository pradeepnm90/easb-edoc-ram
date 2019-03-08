using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IEntityLockRepository:IGenericRepository<grs_PrLckGetLockedItem, int>
    {
        bool LockEntity(int categoryId, int entityId, int userId, out string errorMessage);
		void UnlockEntity (int categoryId, int entityId, int userId);
        IList<grs_PrLckGetLockedItem> GetEntityLocks(int categoryid, int entity, int userId);
    }
}
