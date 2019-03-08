using System.IO;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class AssembliesExtension
    {
        public static string ReadResource(this Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
