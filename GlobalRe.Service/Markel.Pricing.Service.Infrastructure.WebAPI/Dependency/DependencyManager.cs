using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity.WebApi;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    /// <summary>
    /// Represents a class to support Microsoft.Practices.Unity dependency management functionality.
    /// </summary>
    public class DependencyManager : UnityDependencyResolver, IDependencyManager //IDependencyManager, IDependencyResolver//, //, IDependencyResolver
    {
        //protected IUnityContainer Container { get; private set; }
        #region Constructor

        public DependencyManager(IUnityContainer injectedContainer) : base(injectedContainer)
        {
            //this.Container = injectedContainer;
        }

        #endregion

        #region Public Methods

        #region IDependencyManager Implementation

        // NOTE: Commented out portions of methods would be used when automatically trying to resolve be name

        /// <summary>
        /// Resolves the Type t from registration
        /// </summary>
        /// <param name="t">Type to resolve</param>
        /// <returns>Resolved object for Type t</returns>
        public object Resolve(Type t, params ResolverOverride[] resolverOverrides)
        {
            //return this.GetType().GetMethod("Resolve", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(t).Invoke(this, resolverOverrides);

            return Container.Resolve(t, resolverOverrides);
        }

        /// <summary>
        /// Resolve an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of object to get from the container.</typeparam>
        /// <returns>The retrieved object.</returns>
        public T Resolve<T>(params ResolverOverride[] resolverOverrides)
        {
            //var objs = ResolveAll<T>(resolverOverrides).ToList();
            //return objs.Where(i => i.GetType().IsInterface == false).FirstOrDefault();
            ////Exception > 1

            return Container.Resolve<T>(resolverOverrides);
        }

        /// <summary>
        /// Resolve an instance of the requested type with the given name from the container.
        /// </summary>
        /// <typeparam name="T">The type of object to get from the container.</typeparam>
        /// <param name="name">The name</param>
        /// <returns>The retrieved object.</returns>
        public T Resolve<T>(string name, params ResolverOverride[] resolverOverrides)
        {
            return Container.Resolve<T>(name, resolverOverrides);
        }

        /// <summary>
        ///  Return instances of all registered types requested.
        /// </summary>
        /// <typeparam name="T">The type requested.</typeparam>
        /// <returns>Set of objects of type T.</returns>
        public IEnumerable<T> ResolveAll<T>(params ResolverOverride[] resolverOverrides)
        {
            return Container.ResolveAll<T>(resolverOverrides);
        }

        /// <summary>
        /// Register an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="instance">Object to returned.</param>
        public void RegisterInstance<T>(T instance)
        {
            //RegisterInstance<T>(typeof(T).Name, instance);

            Container.RegisterInstance<T>(instance);
        }

        /// <summary>
        ///  Register an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type requested.</typeparam>
        /// <param name="name">Name for registration.</param>
        /// <param name="instance">Object to returned.</param>
        public void RegisterInstance<T>(string name, T instance)
        {
            Container.RegisterInstance<T>(name, instance);
        }

        /// <summary>
        /// Builds the container with the instance and name
        /// </summary>
        /// <param name="instance">Instance to build up</param>
        /// <param name="name">Named parameter for the instance</param>
        /// <returns>Object after BuildUp</returns>
        public object Build(object instance, string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return Container.BuildUp(instance.GetType(), instance);
            }
            else
            {
                return Container.BuildUp(instance.GetType(), instance, name);
            }
        }

        #endregion IDependencyManager Implementation

        #region IDependencyResolver Implementation

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public new IDependencyScope BeginScope()
        {
            var childContainer =  base.Container.CreateChildContainer();
            //childContainer.RegisterInstance<IUnityContainer>(childContainer, new ExternallyControlledLifetimeManager());
            return new UnityDependencyScope(childContainer);

            //var child = Container.CreateChildContainer();           
            //return new DependencyManager(child);
        }

        /// <summary>
        /// Creates Child Container from Parent if it exists, otherwise uses the current container
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public IDependencyManager CreateChildContainer()
        {
            return (IDependencyManager)BeginScope();
        }

        //public object GetService(Type serviceType)
        //{
        //    try
        //    {
        //        return Container.Resolve(serviceType);
        //    }
        //    catch (ResolutionFailedException)
        //    {
        //        return null;
        //    }
        //}

        //public IEnumerable<object> GetServices(Type serviceType)
        //{
        //    try
        //    {
        //        return Container.ResolveAll(serviceType);
        //    }
        //    catch (ResolutionFailedException)
        //    {
        //        return new List<object>();
        //    }
        //}

        //public void Dispose()
        //{
        //    Container.Dispose();
        //}


        #endregion IDependencyResolver Implementation

        #endregion
    }
}
