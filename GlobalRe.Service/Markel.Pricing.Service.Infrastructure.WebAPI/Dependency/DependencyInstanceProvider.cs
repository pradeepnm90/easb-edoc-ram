using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyInstanceProvider : IInstanceProvider, IDisposable
    {
        #region Constructors

        public DependencyInstanceProvider() : this(null)  { } 

        public DependencyInstanceProvider(Type type)
        {
            ServiceType = type;
            dependencyManager = null; // new DependencyManager();
        }

        #region Dispose

        private bool _disposed;

        ~DependencyInstanceProvider()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free other managed objects that implement IDisposable only
                if (dependencyManager != null)
                    dependencyManager.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Dispose

        #endregion

        #region Private Members

        private DependencyManager dependencyManager;

        #endregion

        #region Public Properties

        public Type ServiceType { set; get; }

        #endregion

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return dependencyManager.Resolve(ServiceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null); 
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

        }

        #endregion
    }
}
