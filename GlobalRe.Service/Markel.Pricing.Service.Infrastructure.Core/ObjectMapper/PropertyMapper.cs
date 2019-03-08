using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.ObjectMapper
{
    public static class PropertyMapper
    {
        private static List<Map> MapList = new List<Map>();
        private class Map
        {
            #region Properties

            public Type From { get; private set; }
            public Type To { get; private set; }

            private Dictionary<string, string> propertyMap;
            private List<string> referenceOnlyProperties;

            #endregion Propeties

            #region Constructors

            public Map(Type from, Type to)
            {
                From = from;
                To = to;
                referenceOnlyProperties = new List<string>();
                propertyMap = BuildPropertyMaps(from, to);
            }

            #endregion Constructors

            #region Properties

            public IReadOnlyList<string> PropertyNames
            {
                get { return propertyMap.Keys.Where(k => ReferenceOnlyProperties.Contains(k) == false).ToList().AsReadOnly(); }
            }

            public IReadOnlyList<string> ReferenceOnlyProperties
            {
                get { return referenceOnlyProperties.AsReadOnly(); }
            }

            public string this[string propertyName]
            {
                get
                {
                    if (propertyMap.ContainsKey(propertyName))
                        return propertyMap[propertyName];

                    return null;
                }
            }

            #endregion Properties

            #region Methods

            private /*static*/ Dictionary<string, string> BuildPropertyMaps(Type from, Type to)
            {
                Dictionary<string, string> propertyMaps = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                // Custom Property Maps
                List<MappingAttribute> customMaps = new List<MappingAttribute>();
                customMaps.AddRange(from.Attributes<MappingAttribute>());
                customMaps.AddRange(to.Attributes<MappingAttribute>());

                if (customMaps.Count() > 0)
                {
                    // Left to Right
                    foreach (MappingAttribute map in customMaps.Where(m => m.FromType == from && m.ToType == to))
                    {
                        propertyMaps.Add(map.FromPropertyName, map.ToPropertyName);
                        if (map.IsForReferenceOnly)
                            referenceOnlyProperties.Add(map.FromPropertyName);
                    }

                    // Right to Left
                    foreach (MappingAttribute map in customMaps.Where(m => m.FromType == to && m.ToType == from))
                    {
                        if (!propertyMaps.ContainsKey(map.ToPropertyName))
                        {
                            propertyMaps.Add(map.ToPropertyName, map.FromPropertyName);
                            if (map.IsForReferenceOnly)
                                referenceOnlyProperties.Add(map.ToPropertyName);
                        }
                    }
                }

                // Auto Property Maps
                IList<string> propertyNames = to.GetPropertyNames();
                foreach (string propertyName in from.GetPropertyNames())
                {
                    if (!propertyMaps.Keys.Contains(propertyName) && !propertyMaps.Values.Contains(propertyName) && propertyNames.Contains(propertyName))
                    {
                        propertyMaps.Add(propertyName, propertyName);
                    }
                }

                return propertyMaps;
            }

            #endregion Methods
        }

        private static object MapListLock = new object();
        private static Map GetMap(Type fromType, Type toType)
        {
            lock (MapListLock)
            {
                Map map = MapList.FirstOrDefault(m => m.From == fromType && m.To == toType);
                if (map == null) MapList.Add(map = new Map(fromType, toType));
                return map;
            }
        }

        public static T Translate<F, T>(this F from, T to)
        {
            if (from == null) throw new NullReferenceException($"Cannot Translate on a null object! {typeof(F).ToString()}");
            if (to == null) throw new NullReferenceException($"Cannot Translate to a null object!{typeof(T).ToString()}");

            Map map = GetMap(from.GetType(), to.GetType());

            // Copy Property Values
            foreach (string propertyName in map.PropertyNames)
            {
                from.CopyPropertyValue(to, propertyName, map[propertyName]);
            }

            return to;
        }

        public static string GetPropertyName<F, T>(string propertyName)
        {
            return GetMap(typeof(F), typeof(T))[propertyName] ?? propertyName;
        }

        public static IReadOnlyList<string> GetReferenceOnlyProperties<F, T>()
        {
            Map map = GetMap(typeof(F), typeof(T));
            return (map == null) ? new List<string>().AsReadOnly() : map.ReferenceOnlyProperties;
        }
    }
}
