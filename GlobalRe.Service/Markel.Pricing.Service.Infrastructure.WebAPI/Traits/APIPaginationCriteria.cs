namespace Markel.Pricing.Service.Infrastructure.Traits
{
    public class APIPaginationCriteria
    {
        private const int DEFAULT_PAGE_SIZE = 10;

        // Paging
        public int? Offset { set; get; }
        public int? Limit { set; get; }

        protected int pageSize
        {
            get
            {
                int pageSize = (Limit == null || Limit == 0) ? DEFAULT_PAGE_SIZE : (int)Limit;
                return pageSize;
            }
        }

        protected int pageIndex
        {
            get
            {
                int pageIndex = (Offset != null) ? (int)Offset : 0;
                return (pageIndex / pageSize);
            }
        }

    }
}
