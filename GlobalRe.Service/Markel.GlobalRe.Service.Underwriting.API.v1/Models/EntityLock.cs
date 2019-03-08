using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class EntityLock : BaseApiModel<BLL_EntityLock>
    {
        public EntityLock() { }
        public EntityLock(BLL_EntityLock model) : base(model) { }

        public override BLL_EntityLock ToBLLModel()
        {
            BLL_EntityLock bLL_entryLock = new BLL_EntityLock()
            {
				EntityID = entityId,
				EntityTypeName = entityTypeName,
                LockedTimestamp = lockedTimestamp,
				LockedByDisplayName = lockedByDisplayName
            };
            return bLL_entryLock;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_EntityLock model)
        {
            entityId = model.EntityID;
            entityTypeName = model.EntityTypeName;
            lockedTimestamp = model.LockedTimestamp;
            lockedByDisplayName = model.LockedByDisplayName;
        }
    }
}