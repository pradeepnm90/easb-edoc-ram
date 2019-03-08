using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    [Serializable]
    public class ResponseLookup<API_CLASS> where API_CLASS : IBaseApiModel
    {
        /// <summary>
        /// Request Uri Path and Query
        /// </summary>
        public string url { private set; get; }

        /// <summary>
        /// List of Lookup Values (API Class)
        /// </summary>
        public IList<API_CLASS> results { private set; get; }

        /// <summary>
        /// Total # of Records
        /// </summary>
        public int totalRecords { private set; get; }

        /// <summary>
        /// API Lookup Response
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="results">List of API Models wrapped in an Item (Data, Links, Messages)</param>
        public ResponseLookup(string url, IEnumerable<API_CLASS> results) : this(url, results, results.Count()) { }

        /// <summary>
        /// API Lookup Response
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="results">List of API Models wrapped in an Item (Data, Links, Messages)</param>
        /// <param name="totalRecords">Total # of Records</param>
        public ResponseLookup(string url, IEnumerable<API_CLASS> results, int totalRecords)
        {
            if (string.IsNullOrEmpty(url)) throw new NullReferenceException("URL parameter cannot be NULL!");
            if (results == null) throw new NullReferenceException("Results parameter cannot be NULL!");

            this.url = url;
            this.results = results.ToList();
            this.totalRecords = totalRecords;
        }
    }
}
