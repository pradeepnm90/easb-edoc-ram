using System;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public static class TypeHelper
    {
        public static Type GetType(string typeName)
        {
            if (typeName.Contains("`"))
                return Type.GetType(typeName);

            string[] typeNameParts = typeName.Split(',');
            if (typeNameParts == null || typeNameParts.Length == 0 || typeNameParts.Length > 2)
                return null;
            else if (typeNameParts.Length == 1)
                return Type.GetType(typeNameParts[0]);
            else
                return Assembly.Load(typeNameParts[0].Trim()).GetType(typeNameParts[1].Trim());
        }
    }
}
