using System;
using System.Configuration;

namespace Markel.Pricing.Service.Infrastructure.Config
{
    /// <summary>
    /// Represents a Markel configuration group within a configuration file.
    /// </summary>
    public sealed class MarkelGroup : ConfigurationSectionGroup
    {
        internal static string MarkelGroupName = "markel";
    }
}
