using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IEntityLockManager: IBaseManager
    {
        bool Lock(int categoryid, int entityId, int userId);
        bool Unlock(int categoryid, int entityId, int userId);
        IList<BLL_EntityLock> GetLocks(int categoryid, int enityId, int userId);
    }
}
