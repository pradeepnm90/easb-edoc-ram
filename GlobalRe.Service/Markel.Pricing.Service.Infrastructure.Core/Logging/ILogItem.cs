using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    public interface ILogItem
    {
        LogLevel LogLevel { get; set; }
        DateTimeOffset Timestamp { get; }
        string Title { get; set; }
        string Message { get; set; }
        string LoggerName { get; set; }
        Exception Exception { get; set; }
        int? EventId { get; set; }
        UserIdentity UserIdentity {get; set;}
        Dictionary<string, object> ExtensionElements {get; set;}
        string StackTrace { get; set;}
    }
}
