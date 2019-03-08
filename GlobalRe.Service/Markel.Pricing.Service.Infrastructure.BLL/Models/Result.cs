using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class Result
    {
        private List<Message> _messages = new List<Message>();

        public IReadOnlyCollection<Message> Messages { get { return _messages.AsReadOnly(); } }

        public Result() { }

        public Result(IEnumerable<Message> messages)
        {
            if (messages != null)
                AddRange(messages);
        }

        public Result(Message message)
        {
            if (message != null)
                Add(message);
        }

        public void Add(Message message)
        {
            if (message == null) throw new NullReferenceException("IResponseMessage");
            _messages.Add(message);
        }

        public void AddRange(IEnumerable<Message> messages)
        {
            if (messages == null) throw new NullReferenceException("IList<IResponseMessage>");
            _messages.AddRange(messages);
        }

        public void Add(Result result)
        {
            if (result == null || result.Messages == null) throw new NullReferenceException("IResponseMessage");
            _messages.AddRange(result.Messages);
        }
    }
}
