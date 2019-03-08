using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public enum EntityActionType
    {
        [Description("Entity")]
        Entity,
        [Description("Entity Search")]
        EntitySearch,
        [Description("Related Entity (Top Level Entity)")]
        RelatedEntity,
        [Description("Reference Entity")]
        ReferenceEntity,
        [Description("Sub Entity")]
        SubEntity,
        [Description("Sub Entity Search")]
        SubEntitySearch,
        [Description("Entity Action (Add)")]
        Action_Add,
        [Description("Entity Action (Update)")]
        Action_Update,
        [Description("Entity Action (Partial Update)")]
        Action_Partial_Update,
        [Description("Entity Action (Delete)")]
        Action_Delete,
        [Description("Entity Action (Clone)")]
        Action_Clone
    }
}