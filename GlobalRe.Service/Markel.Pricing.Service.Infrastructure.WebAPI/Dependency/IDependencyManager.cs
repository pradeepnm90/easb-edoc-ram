using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    public interface IDependencyManager
    {
        /// <summary>
        /// Resolves the Type t from registration
        /// </summary>
        /// <param name="t">Type to resolve</param>
        /// <returns>Resolved object for Type t</returns>
        object Resolve(Type t, params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Resolve an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of object to get from the container.</typeparam>
        /// <returns>The retrieved object.</returns>
        T Resolve<T>(params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Resolve an instance of the requested type with the given name from the container.
        /// </summary>
        /// <typeparam name="T">The type of object to get from the container.</typeparam>
        /// <param name="name">The name</param>
        /// <returns>The retrieved object.</returns>
        T Resolve<T>(string name, params ResolverOverride[] resolverOverrides);

        /// <summary>
        ///  Return instances of all registered types requested.
        /// </summary>
        /// <typeparam name="T">The type requested.</typeparam>
        /// <returns>Set of objects of type T.</returns>
        IEnumerable<T> ResolveAll<T>(params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Register an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="instance">Object to returned.</param>
        void RegisterInstance<T>(T instance);

        /// <summary>
        ///  Register an instance with the container.
        /// </summary>
        /// <typeparam name="T">The type requested.</typeparam>
        /// <param name="name">Name for registration.</param>
        /// <param name="instance">Object to returned.</param>
        void RegisterInstance<T>(string name, T instance);

        /// <summary>
        /// Builds the container with the instance and name
        /// </summary>
        /// <param name="instance">Instance to build up</param>
        /// <param name="name">Named parameter for the instance</param>
        /// <returns>Object after BuildUp</returns>
        object Build(object instance, string name = "");

        IDependencyManager CreateChildContainer();
    }
}