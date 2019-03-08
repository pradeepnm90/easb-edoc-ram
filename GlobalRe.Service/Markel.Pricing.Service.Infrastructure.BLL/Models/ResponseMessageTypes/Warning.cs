using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes
{
    /// <summary>
    /// The source or destination resource of a method is locked.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Warning : Message
    {
        public Warning(string field, string detail) : base(field, detail, LogLevel.Warn) { }
        public Warning(Expression<Func<object>> fieldExpression, string detail) : base(fieldExpression, detail, LogLevel.Warn) { }
    }
}
