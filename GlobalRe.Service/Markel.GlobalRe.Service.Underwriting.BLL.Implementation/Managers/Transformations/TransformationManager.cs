using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public abstract class TransformationManager<BLL_CLASS, ENTITY> : ITransformationManager<BLL_CLASS, ENTITY>
        where BLL_CLASS : BaseGlobalReBusinessEntity
        where ENTITY : class
    {
        public virtual BLL_CLASS Transform(ENTITY entity)
        {
            BLL_CLASS bllObj = Activator.CreateInstance<BLL_CLASS>();

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty;

            // Get all of the public properties from Entity with getters and setters
            Dictionary<string, PropertyInfo> bllProperties = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] properties = typeof(BLL_CLASS).GetProperties(flags);
            foreach (PropertyInfo property in properties)
            {
                bllProperties.Add(property.Name, property);
            }

            // Now get all of the public properties from Business Class with getters and setters
            properties = typeof(ENTITY).GetProperties(flags);
            foreach (PropertyInfo entityProperty in properties)
            {
                // If a property matches in name and type, copy across
                if (bllProperties.ContainsKey(entityProperty.Name))
                {
                    PropertyInfo toProperty = bllProperties[entityProperty.Name];
                    if (toProperty.PropertyType == entityProperty.PropertyType)
                    {
                        object value = entityProperty.GetValue(entity, null);
                        toProperty.SetValue(bllObj, value, null);
                    }
                }
            }

            return bllObj;
        }

        public IList<BLL_CLASS> Transform(IList<ENTITY> entities)
        {
            return entities.Select(entity => Transform(entity)).ToList();
        }
    }
}
