using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public class ResponseItem<T> : Item<T> where T : IBaseApiModel
    {
        /// <summary>
        /// Request Uri Path and Query
        /// </summary>
        public string url { private set; get; }

        /// <summary>
        /// Single Item (API Model) wrapped in a response with Request Uri, Links, and Messages
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="data">API Model</param>
        /// <param name="links">List of Links (API Actions)</param>
        /// <param name="messages">List of Response Messages</param>
        public ResponseItem(string url, T data, IList<Link> links, IEnumerable<Message> messages) : base (data, links, messages)
        {
            this.url = url;
        }
    }
}
