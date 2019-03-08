using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class EntityLockTransformationManager : TransformationManager<BLL_EntityLock, grs_PrLckGetLockedItem>, IEntityLockTransformationManager
    {
		public override BLL_EntityLock Transform(grs_PrLckGetLockedItem dbModel)
		{
			return new BLL_EntityLock()
			{
				EntityID = dbModel.ItemID,
				EntityTypeName= null,
				UserID = dbModel.UserID,
				LockedByDisplayName = dbModel.LockingUserName,
				LockingUser = dbModel.LockingUser,
				LockedTimestamp = dbModel.EntryTime
			};
		}

	}
}
