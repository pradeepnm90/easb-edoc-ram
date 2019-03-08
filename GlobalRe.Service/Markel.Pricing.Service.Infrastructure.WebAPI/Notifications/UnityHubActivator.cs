using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;
using System;

namespace Markel.Pricing.Service.Infrastructure.Notifications
{
    public class UnityHubActivator : IHubActivator
    {
        private readonly IUnityContainer _container;

        public UnityHubActivator(IUnityContainer container)
        {
            _container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException("descriptor");
            }

            if (descriptor.HubType == null)
            {
                return null;
            }

            object hub = _container.CreateChildContainer().Resolve(descriptor.HubType) ?? Activator.CreateInstance(descriptor.HubType);
            return hub as IHub;
        }
    }
}
