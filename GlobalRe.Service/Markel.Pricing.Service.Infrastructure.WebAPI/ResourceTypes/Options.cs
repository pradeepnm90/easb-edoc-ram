using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    [Serializable]
    public class Options
    {
        /// <summary>
        /// List of Links (API Actions)
        /// </summary>
        public IList<Link> links { private set; get; }

        /// <summary>
        /// API Options (with Links)
        /// </summary>
        /// <param name="links">List of Links (API Actions)</param>
        public Options(IList<Link> links)
        {
            this.links = links;
        }
    }
}
