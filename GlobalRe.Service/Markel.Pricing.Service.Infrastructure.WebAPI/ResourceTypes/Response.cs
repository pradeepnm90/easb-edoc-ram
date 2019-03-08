using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public class Response
    {
        /// <summary>
        /// Request Uri Path and Query
        /// </summary>
        public string url { private set; get; }

        /// <summary>
        /// List of Response Messages
        /// </summary>
        public IEnumerable<Message> messages { private set; get; }

        /// <summary>
        /// API Response (URL and Messages)
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="messages">List of Response Messages</param>
        public Response(string url, IEnumerable<Message> messages)
        {
            this.url = url;
            this.messages = messages;
        }
    }
}
