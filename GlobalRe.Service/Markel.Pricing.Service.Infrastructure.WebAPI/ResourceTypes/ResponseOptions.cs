using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    [Serializable]
    public class ResponseOptions : Options
    {
        /// <summary>
        /// Request Uri Path and Query
        /// </summary>
        public string url { private set; get; }

        /// <summary>
        /// API Options Response
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="links">List of Links (API Actions)</param>
        public ResponseOptions(string url, IList<Link> links) : base (links)
        {
            this.url = url;
        }
    }
}
