using Elmah;
using Markel.Pricing.Service.Infrastructure;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using System.Collections;

namespace Markel.Pricing.Service.Runtime.GlobalExceptionHandling
{

    public class ElmahErrorLog : SqlErrorLog
    {
        public override string ConnectionString
        {
            get
            {
                IUnityContainer container = UnityConfig.GetConfiguredContainer();
                IUserManager userManager = container.Resolve<IUserManager>();
                return (userManager == null || userManager.UserIdentity == null) ? base.ConnectionString : userManager.UserIdentity.GetConnectionString();                
            }
        }

        public ElmahErrorLog(IDictionary config) : base(config)
        {
        }

        public ElmahErrorLog(string connectionString) : base(connectionString)
        {
        }
    }
}