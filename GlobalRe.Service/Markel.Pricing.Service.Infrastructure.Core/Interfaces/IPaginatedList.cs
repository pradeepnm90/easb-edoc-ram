using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    /// <summary>
    /// Represents a collection of objects that can be individually accessed by index and are
    /// paginated based on the page properties set on the collection.
    /// </summary>
    /// <typeparam name="CLASS">The type of elements in the paginated list. </typeparam>
    public interface IPaginatedList<CLASS> : IPagination
    {
        /// <summary>
        /// Items on this page
        /// </summary>
        IList<CLASS> Items { get; set; }

        CLASS SummaryItem { get; set; }

        IPaginatedList<T> ConvertTo<T>() where T : class;
    }
}
