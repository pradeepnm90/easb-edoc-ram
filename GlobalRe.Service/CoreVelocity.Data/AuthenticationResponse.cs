using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CoreVelocity.Security.Model
{
    [DataContract]
    public class AuthenticationResponse
    {
        [DataMember]
        public bool IsAuthenticated { get; set; }

        [DataMember]
        public Guid? AuthenticationToken { get; set; }

        [DataMember]
        public DateTime? AuthenticationTokenExpires { get; set; }

        [DataMember]
        public string EnvironmentName { get; set; }

        [DataMember]
        public Exception AuthenticationError { get; set; }
    }
}
