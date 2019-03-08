using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public class Item<API_CLASS> where API_CLASS : IBaseApiModel
    {
        /// <summary>
        /// API Model
        /// </summary>
        public API_CLASS data { private set; get; }

        /// <summary>
        /// List of Links (API Actions)
        /// </summary>
        public IList<Link> links { private set; get; }

        /// <summary>
        /// List of Response Messages
        /// </summary>
        public IEnumerable<Message> messages { private set; get; }

        /// <summary>
        /// Initialize Data (API Model) and Links
        /// </summary>
        /// <param name="data">API Model</param>
        /// <param name="links">List of Links (API Actions)</param>
        public Item(API_CLASS data, IList<Link> links)
        {
            this.data = data;
            this.links = links;
        }

        /// <summary>
        /// Initialize Data (API Model), Links, and Messages
        /// </summary>
        /// <param name="data">API Model</param>
        /// <param name="links">List of Links (API Actions)</param>
        /// <param name="messages">List of Response Messages</param>
        public Item(API_CLASS data, IList<Link> links, IEnumerable<Message> messages)
        {
            this.data = data;
            this.links = links;
            if (messages != null)
                this.messages = messages.Select(m => new ResponseMessage(m));
        }
    }
}
