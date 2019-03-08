using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    public enum NotificationLogLevel
    {
        [Description("None")]
        None,
        [Description("Error")]
        Error,
        [Description("Info")]
        Info,
        [Description("Success")]
        Success,
        [Description("Warning")]
        Warning
    }
}
