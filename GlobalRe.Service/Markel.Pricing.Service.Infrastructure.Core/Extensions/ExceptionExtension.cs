using Markel.Pricing.Service.Infrastructure.Exceptions;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Add Error Message
        /// </summary>
        /// <param name="detail">Message Detail</param>
        /// <param name="args">Parameters (String Format)</param>
        public static void Add(this IList<APIMessage> messages, string field, string detail, params object[] args)
        {
            messages.Add(field, string.Format(detail, args));
        }

        /// <summary>
        /// Adds a new message to a collection of API Messages
        /// </summary>
        /// <param name="messages">List of API Messages</param>
        /// <param name="field">Field Name</param>
        /// <param name="detail">Message Details</param>
        public static void Add(this IList<APIMessage> messages, string field, string detail)
        {
            messages.Add(new APIMessage(field, detail));
        }
    }
}
