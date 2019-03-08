namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    /// <summary>
    /// Represents common pagination settings for an object or collection of objects.
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// Gets or sets the number of records per page to return.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the current page number the record set is on.
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Gets the current page number. (Calculated using "PageIndex + 1")
        /// </summary>
        int PageNumber { get; }
        
        /// <summary>
        /// Gets or sets the total number of pages in the recordset
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of records in the recordset.
        /// </summary>
        int TotalRecordCount { get; set; }       
    }
}
