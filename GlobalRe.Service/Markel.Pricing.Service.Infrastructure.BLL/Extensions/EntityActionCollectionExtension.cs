using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class EntityActionCollectionExtension
    {
        public static void Add(this IList<EntityAction> entityActions, Enum entityType, long? id, EntityActionType actionType)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>()
            {
                actionType
            };

            entityActions.Add(new EntityAction(
                entityType: entityType,
                entityId: id,
                entityActionTypes: entityActionTypes
            ));
        }

        public static void Add(this IList<EntityAction> entityActions, Enum entityType, string key, EntityActionType actionType)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>()
            {
                actionType
            };

            entityActions.Add(new EntityAction(
                entityType: entityType,
                entityKey: key,
                entityActionTypes: entityActionTypes
            ));
        }

        public static void Add(this IList<EntityAction> entityActions, Enum entityType, EntityActionType actionType)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>()
            {
                actionType
            };

            entityActions.Add(new EntityAction(
                entityType: entityType,
                entityActionTypes: entityActionTypes
            ));
        }
    }
}
