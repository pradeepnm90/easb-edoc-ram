using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        public static bool IsNullOrEmpty(this object token)
        {
            if (token is JValue)
            {
                JValue value = token as JValue;
                return (value == null) ||
                  (value.Type == JTokenType.Array && !value.HasValues) ||
                  (value.Type == JTokenType.Object && !value.HasValues) ||
                  (value.Type == JTokenType.String && value.ToString() == String.Empty) ||
                  (value.Type == JTokenType.Null);
            }
            return token == null;
        }

        public static string ToJsonString(this object data, bool serializeType = true)
        {
            return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = serializeType ? TypeNameHandling.All : TypeNameHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public static object JsonToType(this string json, Type conversion, bool serializeType = true)
        {
            if (string.IsNullOrEmpty(json)) return null;

            return JsonConvert.DeserializeObject(json, conversion, new JsonSerializerSettings
            {
                TypeNameHandling = serializeType ? TypeNameHandling.All : TypeNameHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public static T JsonToType<T>(this string json, bool serializeType = true)
        {
            object value = json.JsonToType(typeof(T), serializeType) ?? default(T);
            return (T)value;
        }
    }
}
