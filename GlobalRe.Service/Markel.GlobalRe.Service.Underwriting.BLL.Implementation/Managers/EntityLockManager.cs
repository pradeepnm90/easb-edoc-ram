using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class EntityLockManager : BaseGlobalReManager<BLL_EntityLock>,IEntityLockManager
    {
        private static object syncRoot = new Object();
        private IEntityLockRepository _entityLockRepository = null;
        private IEntityLockTransformationManager _entityLockTransformationManager = null;
        public EntityLockManager(IUserManager userManager, 
                               ICacheStoreManager cacheStoreManager, 
                               ILogManager logManager,
                               IEntityLockRepository entityLockRepository,
                               IEntityLockTransformationManager entityLockTransformationManager) : base(userManager, cacheStoreManager, logManager)
        {
            _entityLockRepository = ValidateRepository(entityLockRepository);
            _entityLockTransformationManager = entityLockTransformationManager ?? throw new NullReferenceException(typeof(IEntityLockTransformationManager).ToString());           
        }

		public bool Lock(int categoryId, int entityId, int userId)
		{
			bool entityLocked = false;
			string errorMessage;

			var entityLocks = GetLocks(categoryId, entityId, userId);
			if (entityLocks?.Count > 0 && entityLocks.Count(e => e.UserID == UserIdentity.UserId) <= 0)
				throw new NotAllowedAPIException($"{entityLocks.FirstOrDefault().LockedByDisplayName} has locked this deal for edit");
			lock (syncRoot)
			{
				entityLocks = GetLocks(categoryId, entityId, userId);
				if (entityLocks?.Count > 0 && entityLocks.Count(e => e.UserID == UserIdentity.UserId) <= 0)
					throw new NotAllowedAPIException($"{entityLocks.FirstOrDefault().LockedByDisplayName} has locked this deal for edit");
				entityLocked = _entityLockRepository.LockEntity(categoryId, entityId, userId, out errorMessage);
				if (!string.IsNullOrEmpty(errorMessage))
					throw new NotAllowedAPIException(errorMessage);
			}
			return entityLocked;
		}

        public bool Unlock(int categoryId, int entityId, int userId)
        {
           _entityLockRepository.UnlockEntity(categoryId, entityId, userId);
			var locks = GetLocks(categoryId, entityId, userId);
			bool result = (locks == null) || (locks.Count == 0);
			return result;
        }

        public IList<BLL_EntityLock> GetLocks(int categoryId, int entityId, int userId)
        {
			var data = _entityLockRepository.GetEntityLocks(categoryId, entityId, userId);
			if (data.Count == 0) return null;
			return _entityLockTransformationManager.Transform(data);
        }


	}

}
