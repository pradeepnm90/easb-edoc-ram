using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Xml.Serialization;
using Markel.Pricing.Service.Infrastructure.Exceptions;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    [DataContract, KnownType(typeof(Error)), KnownType(typeof(Fatal)), KnownType(typeof(Information)), KnownType(typeof(ServiceUnavailable)), KnownType(typeof(Warning))]
    public abstract class BaseMessageEntity : BaseBusinessEntity, IMessageEntity
    {
        [XmlAttribute]
        [DataMember]
        private List<Message> messages = new List<Message>();

        public IReadOnlyCollection<Message> Messages { get { return messages.AsReadOnly(); } set { messages = (List<Message>)value; } }

        public BaseMessageEntity() { }

        public BaseMessageEntity(IEnumerable<Message> messages)
        {
            if (messages != null) AddRange(messages);
        }

        public void ClearMessages()
        {
            messages.Clear();
        }

        public void Add(Message message)
        {
            if (message == null) throw new NullReferenceException(typeof(Message).ToString());
            if (!messages.Any(m => m.field == message.field && m.detail == message.detail && m.Severity == message.Severity))
                messages.Add(message);
        }

        public void AddRange(IEnumerable<Message> messages)
        {
            if (messages == null) throw new NullReferenceException(typeof(IEnumerable<Message>).ToString());
            foreach (var message in messages)
            {
                this.messages.Add(message);
            }
        }

        public bool HasMessages()
        {
            return (Messages.Count == 0) ? false : true;
        }

        public bool HasWarning()
        {
            return Messages.HasWarning();
        }

        public bool HasErrorOrFatal()
        {
            return Messages.HasErrorOrFatal();
        }

        public bool HasError()
        {
            return Messages.HasError();
        }

        public bool HasFatal()
        {
            return Messages.HasFatal();
        }

        public IList<APIMessage> GetFatalMessages()
        {
            return Messages.GetFatalMessages();
        }
    }
}
