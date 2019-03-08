using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes
{
    /// <summary>
    /// Generic Response Message
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Message : APIMessage
    {
        [XmlAttribute]
        [DataMember]
        public string Severity { get; private set; }

        internal Message(string field, string detail, LogLevel severity) : base(field, detail)
        {
            Severity = severity.ToString();
        }

        protected Message(string field, string detail, string severity) : base(field, detail)
        {
            Severity = severity;
        }

        protected Message(Expression<Func<object>> fieldExpression, string detail, LogLevel severity) : base(fieldExpression, detail)
        {
            Severity = severity.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0}: [{1}] {3}", Severity, field, string.IsNullOrWhiteSpace(detail)? "" : " - ", detail);
        }
    }
}
