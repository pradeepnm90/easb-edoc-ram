using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    [Serializable]
    public class ResponseCollection<API_CLASS> where API_CLASS : IBaseApiModel
    {
        /// <summary>
        /// Request Uri Path and Query
        /// </summary>
        public string url { private set; get; }

        /// <summary>
        /// List of API Models wrapped in an Item (Data, Links, Messages)
        /// </summary>
        public IList<Item<API_CLASS>> results { private set; get; }
        public Item<API_CLASS> SummaryResult { private set; get; }

        /// <summary>
        /// Total # of Records
        /// </summary>
        public int totalRecords { private set; get; }

        /// <summary>
        /// API Response Collection
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="results">List of API Models wrapped in an Item (Data, Links, Messages)</param>
        public ResponseCollection(string url, IEnumerable<Item<API_CLASS>> results) : this(url, results, results.Count()) { }

        /// <summary>
        /// API Response Collection
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="results">List of API Models wrapped in an Item (Data, Links, Messages)</param>
        /// <param name="totalRecords">Total # of Records</param>
        public ResponseCollection(string url, IEnumerable<Item<API_CLASS>> results, int totalRecords)
        {
            if (string.IsNullOrEmpty(url)) throw new NullReferenceException("URL parameter cannot be NULL!");
            if (results == null) throw new NullReferenceException("Results parameter cannot be NULL!");

            this.url = url;
            this.results = results.ToList();
            this.totalRecords = totalRecords;
        }

        /// <summary>
        /// API Response Collection
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="results">List of API Models wrapped in an Item (Data, Links, Messages)</param>
        /// <param name="totalRecords">Total # of Records</param>
        public ResponseCollection(string url, IEnumerable<Item<API_CLASS>> results, Item<API_CLASS> summaryResult, int totalRecords)
        {
            if (string.IsNullOrEmpty(url)) throw new NullReferenceException("URL parameter cannot be NULL!");
            if (results == null) throw new NullReferenceException("Results parameter cannot be NULL!");

            this.url = url;
            this.results = results.ToList();
            this.SummaryResult = summaryResult;
            this.totalRecords = totalRecords;
        }

        [Obsolete("Messages is now part of the Item<API_CLASS>")]
        public IList<Message> messages { private set; get; }
        [Obsolete("Messages is now part of the Item<API_CLASS>")]
        public ResponseCollection(string url, IEnumerable<Item<API_CLASS>> results, IEnumerable<Message> messages, int totalRecords)
        {
            this.url = url;
            this.results = results.ToList();
            this.totalRecords = totalRecords;
            this.messages = messages.ToList();
        }
    }
}
