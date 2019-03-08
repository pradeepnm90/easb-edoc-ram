using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public enum LinkType
    {
        [Description("Entity")]
        Entity,
        [Description("Related Entity")]
        RelatedEntity,
        [Description("Reference Entity")]
        ReferenceEntity,
        [Description("Sub Entity")]
        SubEntity,
        [Description("Action")]
        Action
    }
}
