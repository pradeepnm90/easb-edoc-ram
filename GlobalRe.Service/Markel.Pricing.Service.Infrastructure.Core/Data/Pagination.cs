using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    /// <summary>
    /// Represents common pagination settings for an object or collection of objects.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Pagination : IPagination
    {
        public Pagination() { }

        public Pagination(SearchCriteria searchCriteria)
        {
            PageSize = searchCriteria.PageSize;
            PageIndex = searchCriteria.PageIndex;
        }

        /// <summary>
        /// Gets or sets the number of records per page to return.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the current page index the record set is on.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets the current page the record set is on, this is
        /// calculated using PageIndex + 1 since the index is a 
        /// zero based indexer.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageNumber
        {
            get
            {
                return PageIndex + 1;
            }
        }

        /// <summary>
        /// Gets or sets the total number of pages in the
        /// </summary>
        [XmlElement]
        [DataMember]
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of records in the recordset.
        /// </summary>
        [XmlElement]
        [DataMember]
        public int TotalRecordCount { get; set; }
    }
}
