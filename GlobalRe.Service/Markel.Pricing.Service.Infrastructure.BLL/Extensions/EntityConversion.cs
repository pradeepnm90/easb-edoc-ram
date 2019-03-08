using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class EntityConversion
    {
        /// <summary>
        /// Applies changes to entity and returns a list of changed fields
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="entity">Entity to apply changes to</param>
        /// <param name="changes">Change List and Values</param>
        /// <returns>List of changed fields</returns>
        public static IList<string> ApplyChanges<T>(this T entity, IList<EntityChange> changes)
        {
            IList<string> changeList = new List<string>();
            foreach (var field in changes)
            {
                // Use reflection to validate field exists on T and apply new value
                IEnumerable<PropertyInfo> runtimeProperties = entity.GetType().GetRuntimeProperties();
                var property = runtimeProperties.FirstOrDefault(p => p.Name.Equals(field.PropertyName, StringComparison.InvariantCultureIgnoreCase));
                if (property != null)
                {
                    object value = field.Value.ChangeType(property.PropertyType);
                    property.SetValue(entity, value, null);
                    changeList.Add(property.Name);
                }
            }

            return changeList;
        }

        /// <summary>
        /// Matches PropertyName field in change list to the generic types property names.
        /// This will fix PropertyName case and will only return a list of valid field mappings.
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="changes">Change List and Values</param>
        /// <returns>List of changes</returns>
        public static IList<EntityChange> ToEntityChangeList<T>(this IList<EntityChange> changes)
        {
            IList<EntityChange> changeList = new List<EntityChange>();
            foreach (var field in changes)
            {
                // Use reflection to validate field exists on T and apply new value
                IEnumerable<PropertyInfo> runtimeProperties = typeof(T).GetRuntimeProperties();
                var property = runtimeProperties.FirstOrDefault(p => p.Name.Equals(field.PropertyName, StringComparison.InvariantCultureIgnoreCase));
                if (property != null)
                {
                    object value = field.Value.ChangeType(property.PropertyType);
                    changeList.Add(new EntityChange(property.Name, value));
                }
            }

            return changeList;
        }

    }
}
