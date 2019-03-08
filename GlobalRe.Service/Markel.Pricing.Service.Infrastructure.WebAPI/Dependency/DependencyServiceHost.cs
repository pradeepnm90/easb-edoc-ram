using System;
using System.ServiceModel;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    /// <summary>
    /// Dependency Service Host
    /// </summary>
    public class DependencyServiceHost : ServiceHost
    {

        #region Constrcutors

        public DependencyServiceHost()
            : base()
        {
            
        }

        public DependencyServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            
        }

        #endregion

        #region ServiceHost Override Methods

        protected override void OnOpening()
        {
            if (this.Description.Behaviors.Find<DependencyServiceBehavior>() == null)
            {
                this.Description.Behaviors.Add(new DependencyServiceBehavior()); 
            }
            base.OnOpening();
        }

        #endregion

    }
}
