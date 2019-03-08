namespace Markel.Pricing.Service.Infrastructure.Security
{
    /// <summary>
    /// Authentication is the verification of the credentials of the connection attempt. This process consists of sending the credentials
    /// from the remote access client to the remote access server in an either plaintext or encrypted form by using an authentication protocol. 
    /// </summary>
    public interface IAuthenticationManager
    {
        bool Authenticate(string userName, string password, string domainName);
    }
}
