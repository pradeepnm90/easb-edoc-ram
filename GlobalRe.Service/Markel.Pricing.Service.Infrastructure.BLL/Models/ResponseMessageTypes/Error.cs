using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes
{
    /// <summary>
    /// The source or destination resource of a method is locked.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Error : Message
    {
        public Error(string field, string detail) : base(field, detail, LogLevel.Error) { }
        public Error(string field, string detail, params object[] args) : this(field, string.Format(detail, args)) { }
        public Error(string field, string detail, string comment) : this(field, detail + ": " + comment) { }
    }
}
