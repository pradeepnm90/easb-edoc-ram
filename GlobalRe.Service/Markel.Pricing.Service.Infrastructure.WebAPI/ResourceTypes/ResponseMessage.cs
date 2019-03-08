using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public class ResponseMessage : Message
    {
        internal ResponseMessage(Message message) : base(message.field.LowerCaseFirstCharacter(), message.detail, message.Severity) { }
    }
}
