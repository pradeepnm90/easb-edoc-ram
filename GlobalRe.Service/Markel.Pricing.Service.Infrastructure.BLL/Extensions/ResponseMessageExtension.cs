using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class ResponseMessageExtension
    {
        public static bool HasWarning(this Result result)
        {
            return (result == null)? false : result.Messages.HasWarning();
        }

        public static bool HasError(this Result result)
        {
            return (result == null) ? false : result.Messages.HasError();
        }

        public static bool HasFatal(this Result result)
        {
            return (result == null) ? false : result.Messages.HasFatal();
        }

        public static bool HasErrorOrFatal(this Result result)
        {
            return (result == null) ? false : result.Messages.HasFatal() || result.Messages.HasError();
        }

        public static bool HasWarning(this IEnumerable<Message> messages)
        {
            var warning = LogLevel.Warn.ToString();
            return (messages == null) ? false : messages.Any(m => m is Warning || m.Severity == warning);
        }

        public static bool HasError(this IEnumerable<Message> messages)
        {
            var error = LogLevel.Error.ToString();
            return (messages == null) ? false : messages.Any(m => m is Error || m.Severity == error);
        }

        public static bool HasFatal(this IEnumerable<Message> messages)
        {
            var fatal = LogLevel.Fatal.ToString();
            return (messages == null) ? false : messages.Any(m => m is Fatal || m.Severity == fatal);
        }

        public static bool HasErrorOrFatal(this IEnumerable<Message> messages)
        {
            var error = LogLevel.Error.ToString();
            var fatal = LogLevel.Fatal.ToString();
            return (messages == null) ? false : messages.Any(m => m is Error || m.Severity == error || m is Fatal || m.Severity == fatal);
        }

        public static IList<APIMessage> GetFatalMessages(this IEnumerable<Message> messages)
        {
            return messages.GetMessages(LogLevel.Fatal);
        }

        public static IList<APIMessage> GetMessages(this IEnumerable<Message> messages, params LogLevel[] filteredLogLevels)
        {
            IList<string> logLevels = filteredLogLevels.Select(l => l.ToString()).ToList();
            IEnumerable<Message> messageList = messages.Where(m => logLevels.Contains(m.Severity));
            return messageList.Select(m => m as APIMessage).ToList();
        }

        public static IList<string> GetMessagesAsText(this IEnumerable<Message> messages, params LogLevel[] filteredLogLevels)
        {
            IEnumerable<APIMessage> filteredMessages = GetMessages(messages, filteredLogLevels);

            return filteredMessages.Select(m => string.Format("{0}: {1}{2}", m.field, string.IsNullOrWhiteSpace(m.detail) ? "" : ": ", m.detail)).ToList();
        }
    }
}
