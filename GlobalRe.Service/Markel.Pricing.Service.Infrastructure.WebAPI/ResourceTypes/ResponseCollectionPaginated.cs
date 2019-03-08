using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    [Serializable]
    public class ResponsePaginatedCollection<API_CLASS> : ResponseCollection<API_CLASS> where API_CLASS : IBaseApiModel
    {
        /// <summary>
        /// Paging Offset
        /// </summary>
        public int? offset { private set; get; }

        /// <summary>
        /// Paging Limit (# of records per page)
        /// </summary>
        public int? limit { private set; get; }

        /// <summary>
        /// API Paginated Response Collection
        /// </summary>
        /// <param name="url">Request Uri Path and Query</param>
        /// <param name="results">List of API Models wrapped in an Item (Data, Links, Messages)</param>
        /// <param name="totalRecords">Total # of Records</param>
        /// <param name="offset">Paging Offset</param>
        /// <param name="limit">Paging Limit (# of records per page)</param>
        public ResponsePaginatedCollection(string url, IEnumerable<Item<API_CLASS>> results, Item<API_CLASS> summaryResult, int totalRecords, int? offset, int? limit)
            : base(url, results, summaryResult, totalRecords)
        {
            this.offset = offset;
            this.limit = limit;
        }

        [Obsolete("Messages is now part of the Item<API_CLASS>")]
        public ResponsePaginatedCollection(string url, IEnumerable<Item<API_CLASS>> results, IEnumerable<Message> messages, int totalRecords, int? offset, int? limit)
            : base(url, results, messages, totalRecords)
        {
            this.offset = offset;
            this.limit = limit;
        }
    }
}
