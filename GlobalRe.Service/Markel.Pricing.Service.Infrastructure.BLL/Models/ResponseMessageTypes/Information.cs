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
    public class Information : Message
    {
        public Information(string field, string detail) : base(field, detail, LogLevel.Info) { }
    }
}
