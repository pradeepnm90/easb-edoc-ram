using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    /// <summary>
    /// Dependency injection Service Behavior
    /// </summary>
    public class DependencyServiceBehavior : IServiceBehavior
    {

        #region Constructors

        public DependencyServiceBehavior()
        {
            InstanceProvider = new DependencyInstanceProvider();
        }

        #endregion

        #region Public Properties

        public DependencyInstanceProvider InstanceProvider { get; set; }

        #endregion

        #region IServiceBehavior Implementation

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        InstanceProvider.ServiceType = serviceDescription.ServiceType;
                        ed.DispatchRuntime.InstanceProvider = InstanceProvider; 
                    }
                }
            }
        }

        #endregion
    }
}
