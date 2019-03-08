using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    internal static class Check
    {
        public static void ArgumentNotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null) throw new ArgumentNullException(argumentName);
        }
    }
}
