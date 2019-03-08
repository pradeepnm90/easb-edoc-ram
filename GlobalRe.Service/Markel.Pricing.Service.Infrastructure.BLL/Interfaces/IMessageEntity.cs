using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    public interface IMessageEntity : IBusinessEntity
    {
        IReadOnlyCollection<Message> Messages { get; set; }
        void Add(Message message);
        bool HasWarning();
        bool HasError();
        bool HasFatal();
    }
}
