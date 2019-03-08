using Markel.Pricing.Service.Infrastructure.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes
{
    /// <summary>
    /// The source or destination resource of a method is locked.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ServiceUnavailable : Warning
    {
        public ServiceUnavailable(string detail) : base("Service is Unavailable", detail) { }
        public ServiceUnavailable(string detail, params object[] args) : this(string.Format(detail, args)) { }
        public ServiceUnavailable(ServiceUnavailableAPIException ex) : this(ex.Message) { }
    }
}
