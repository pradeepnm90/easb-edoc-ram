using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public class EntityAction
    {
        #region Private Variables

        private List<EntityAction> _entityActions = new List<EntityAction>();
        private List<EntityActionType> _entityActionTypes = new List<EntityActionType>();

        #endregion Private Variables

        #region Properties

        public Enum EntityType { get; private set; }
        public virtual ReadOnlyCollection<EntityActionType> EntityActionTypes { get { return _entityActionTypes.AsReadOnly(); } }
        public virtual ReadOnlyCollection<EntityAction> EntityActions { get { return _entityActions.AsReadOnly(); } }
        public string EntityKey { get; private set; }

        #endregion Properties

        #region Constructors

        public EntityAction(Enum entityType, string entityKey, IList<EntityActionType> entityActionTypes, IList<EntityAction> entityActions = null)
        {
            EntityType = entityType;
            EntityKey = entityKey;

            AddRange(entityActionTypes);
            AddRange(entityActions);
        }

        public EntityAction(Enum entityType, string entityKey, IList<EntityAction> entityActions) :
            this(
                entityType: entityType,
                entityKey: entityKey,
                entityActionTypes: null,
                entityActions: entityActions
            )
        { }

        public EntityAction(Enum entityType, string entityKey) :
            this(
                entityType: entityType,
                entityKey: entityKey,
                entityActionTypes: null,
                entityActions: null
            )
        { }

        public EntityAction(Enum entityType, long? entityId, IList<EntityActionType> entityActionTypes, IList<EntityAction> entityActions) :
            this(
                entityType: entityType,
                entityKey: entityId.HasValue ? entityId.ToString() : null,
                entityActionTypes: entityActionTypes,
                entityActions: entityActions
            )
        { }

        public EntityAction(Enum entityType, long? entityId, IList<EntityActionType> entityActionTypes) :
            this(
                entityType: entityType,
                entityKey: entityId.HasValue ? entityId.ToString() : null,
                entityActionTypes: entityActionTypes,
                entityActions: null
            )
        { }

        public EntityAction(Enum entityType, long? entityId, IList<EntityAction> entityActions) :
            this(
                entityType: entityType,
                entityKey: entityId.HasValue ? entityId.ToString() : null,
                entityActionTypes: null,
                entityActions: entityActions
            )
        { }

        public EntityAction(Enum entityType, long? entityId) :
            this(
                entityType: entityType,
                entityKey: entityId.HasValue ? entityId.ToString() : null,
                entityActionTypes: null,
                entityActions: null
            )
        { }

        public EntityAction(Enum entityType, IList<EntityActionType> entityActionTypes) :
            this(
                entityType: entityType,
                entityKey: null,
                entityActionTypes: entityActionTypes,
                entityActions: null
            )
        { }

        public EntityAction(Enum entityType, EntityActionType entityActionType) :
            this(
                entityType: entityType,
                entityKey: null,
                entityActionTypes: new List<EntityActionType>() { entityActionType },
                entityActions: null
            )
        { }

        public EntityAction(Enum entityType) :
            this(
                entityType: entityType,
                entityKey: null,
                entityActionTypes: null,
                entityActions : null
            )
        { }

        #endregion Constructors

        #region Methods

        public void Add(EntityActionType entityActionTypes)
        {
            _entityActionTypes.Add(entityActionTypes);
        }

        public void AddRange(IList<EntityActionType> entityActionTypes)
        {
            if (entityActionTypes != null && entityActionTypes.Count > 0)
            {
                _entityActionTypes.AddRange(entityActionTypes);
            }
        }

        public void Add(EntityAction entityActions)
        {
            _entityActions.Add(entityActions);
        }

        public void AddRange(IList<EntityAction> entityActions)
        {
            if (entityActions != null && entityActions.Count > 0)
            {
                _entityActions.AddRange(entityActions);
            }
        }

        public override string ToString()
        {
            return $"{EntityType}: {EntityKey}";
        }

        #endregion Methods
    }
}
