using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Markel.Pricing.Service.Infrastructure
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer().AddNewExtension<Interception>();
            //DisposeExtension disposeExtension = new DisposeExtension();
            //container.AddExtension(disposeExtension);
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            string moduleFilePath = AppDomain.CurrentDomain.RelativeSearchPath;

            var assemblyTypeMappings = LoadAssemblies(moduleFilePath,
                MarkelConfiguration.ModuleFilter, MarkelConfiguration.TypeFilter, MarkelConfiguration.LoadModuleConfigFiles);

            var registerTypeList = AllClasses.FromLoadedAssemblies().Where(t => !string.IsNullOrEmpty(t.Namespace) &&
                          (t.Namespace.StartsWith("Markel.") || t.Namespace.StartsWith("Markel.Service")) &&
                          (
                            t.Name.EndsWith("Manager") || t.GetInterfaces().Contains(typeof(IBaseManager)) ||
                            t.Name.EndsWith("Repository") ||
                            t.Name.EndsWith("Provider") ||
                            t.Name.EndsWith("Factory") ||
                            t.Name.EndsWith("DbContext") /*|| t.Name.EndsWith("Logger")*/
                          )
                      ).OrderBy(k => k.Name).ToList();

            //ETM_FIX: Regex is not working for matching type names, but the match works in poc. Once resolved this file can be moved to Markel.Pricing.Infrastructure
            Regex registerTypeRegex = (string.IsNullOrEmpty(MarkelConfiguration.RegisterTypeFilter)) ? null : new Regex(MarkelConfiguration.RegisterTypeFilter);
            var registerTypeList2 = AllClasses.FromLoadedAssemblies()
                .Where(t => !string.IsNullOrEmpty(t.Namespace) && registerTypeRegex != null && registerTypeRegex.IsMatch(t.Namespace) == true)
                .ToList();

            container.RegisterTypes(registerTypeList, WithMappings.FromMatchingInterface, WithName.Default, WithLifetime.Hierarchical);

            UnityConfigurationSection unitySection = (UnityConfigurationSection)ConfigurationManager.GetSection(MarkelConfiguration.UnityConfigSectionName);
            unitySection.Configure(container, MarkelConfiguration.UnityContainerName);

            //container.RegisterTypes(registerTypeList, WithMappings.FromMatchingInterface, WithName.Default, WithLifetime.Hierarchical);

            // Below line allows mapping of interface to multiple types (use instead of above line
            //container.RegisterTypes(registerTypeList, WithMappings.FromAllInterfaces, WithName.TypeName, WithLifetime.Transient);

            //ETM_VERIFY: That we do not need to register the types because they are being registered above.
            //Register the dependencies found when loading assemblies.
            //foreach (var mapping in assemblyTypeMappings)
            //{
            //    container.RegisterType(mapping.Key, mapping.Value, new TransientLifetimeManager());
            //}

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }

        /// <summary>
        /// Loads assemblies from specidied file path.
        /// </summary>
        /// <param name="moduleFilePath">The file path.</param>
        /// <param name="moduleFilter">The module filter.</param>
        /// <param name="interfaceFilter">The interface filter.</param>
        /// <returns></returns>
        /// <exception cref="Microsoft.Practices.Unity.DuplicateTypeMappingException"></exception>
        private static Dictionary<Type, Type> LoadAssemblies(string moduleFilePath, string moduleFilter, string typeFilter, bool loadModuleConfigFile = false)
        {
            Dictionary<Type, Type> assemblyTypeMappings = new Dictionary<Type, Type>();
            List<string> loadedAssemblyNames = new List<string>();

            if (string.IsNullOrEmpty(moduleFilePath) || string.IsNullOrEmpty(moduleFilter))
                return assemblyTypeMappings;

            // Get list of currently loaded assemblies (this is used to avoid duplicate loading of assembly)
            var loadedAssemblies = GetDependentAssemblies(null).OrderBy(o => o.FullName).ToList();
            foreach (var a in loadedAssemblies)
            {
                if (!a.IsDynamic)
                    loadedAssemblyNames.Add(Path.GetFileNameWithoutExtension(a.Location));
            }

            if (Directory.Exists(moduleFilePath))
            {
                // Initialize regular expression filters
                Regex moduleFilterRegex = new Regex(moduleFilter);
                Regex typeFilterRegex = string.IsNullOrEmpty(typeFilter) ? null : new Regex(typeFilter);

                // Get list of assemblies to load, excluding any alreay loaded assemblies
                string[] Dependencies = Directory.GetFiles(moduleFilePath)
                    .Where(f => moduleFilterRegex.IsMatch(f) && !loadedAssemblyNames.Contains(Path.GetFileNameWithoutExtension(f)))
                    .ToArray();

                //Load Dependency Assemblies
                foreach (string fileName in Dependencies)
                {
                    var moduleAssembly = Assembly.LoadFrom(fileName);
                    var refferencedAssemblies = moduleAssembly.GetReferencedAssemblies();

                    if (loadModuleConfigFile == true)
                    {
                        LoadUnityConfiguration(fileName);
                    }
                    else
                    {
                        //ETM_VERIFY: That we do not need to register the types because that are being done RegisterTypes
                        //Only look at public types which are non-abstract types
                        foreach (Type pluginType in moduleAssembly.GetTypes())
                        {
                            if (pluginType.IsPublic && !pluginType.IsAbstract)  //Only look at non-abstract types
                            {
                                //Gets a type object of the interface we need the plugins to match
                                var typeInterfaces = pluginType.GetInterfaces()
                                    //.Where(i => typeFilterRegex == null || (typeFilterRegex.IsMatch(i.FullName) && i.Name != "IBaseRepository"))
                                    .Where(i => typeFilterRegex == null || (i.FullName != null && typeFilterRegex.IsMatch(i.FullName)))
                                    .ToList();
                                foreach (Type typeInterface in typeInterfaces)
                                {
                                    if (assemblyTypeMappings.ContainsKey(typeInterface))
                                    {
                                        throw new DuplicateTypeMappingException(typeInterface.Name, typeInterface, assemblyTypeMappings[typeInterface], pluginType);
                                    }
                                    assemblyTypeMappings.Add(typeInterface, pluginType);
                                }
                            }
                        }
                    }
                }
            }

            return assemblyTypeMappings;
        }

        /// <summary>
        /// Loads the unity configuration section from module configuration file for specified module. The name of the unity configuration 
        /// file is determined by replacing the module file extension with the module unity configuraiton extension configured.
        /// </summary>
        /// <param name="moduleFilePathName">Name of the module file path.</param>
        private static void LoadUnityConfiguration(string moduleFilePathName)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.ChangeExtension(moduleFilePathName, MarkelConfiguration.ModuleUnityConfigExtension) };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection(MarkelConfiguration.UnityConfigSectionName);
            //var container = new UnityContainer().LoadConfiguration(unitySection);
            unitySection.Configure(GetConfiguredContainer(), MarkelConfiguration.UnityContainerName);
        }

        private static IEnumerable<Assembly> GetDependentAssemblies(Assembly analyzedAssembly)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(a => analyzedAssembly == null || GetNamesOfAssembliesReferencedBy(a).Contains(analyzedAssembly.FullName));
        }

        public static IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Select(assemblyName => assemblyName.FullName);
        }
    }
}