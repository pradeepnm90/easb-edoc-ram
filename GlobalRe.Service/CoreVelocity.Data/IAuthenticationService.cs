using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CoreVelocity.Core.Security;
using CoreVelocity.Security.Model;

namespace CoreVelocity.Security.Service
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract(Name = "Authenticate")]
        EnterpriseIdentity Authenticate(EnterpriseIdentity identityToAuthenticate);

        [OperationContract(Name = "AuthenticateWithParams")]
        AuthenticationResponse Authenticate(string userName, string domainName, string environmentName, string applicationName);

        [OperationContract(Name = "ValidateAuthenticationToken")]
        bool ValidateAuthenticationToken(EnterpriseIdentity identityToValidate);

        [OperationContract(Name = "ValidateAuthenticationTokenWithParams")]
        bool ValidateAuthenticationToken(string token, string environmentName);

        [OperationContract]
        EnterpriseIdentity GetIdentityFromToken(string token, string environmentName);
    }
}
