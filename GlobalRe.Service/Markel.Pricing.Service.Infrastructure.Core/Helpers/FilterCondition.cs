namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public class FilterCondition
    {
        #region Properties

        public string DataType { get; set; }
        public string DbFieldName { get; set; }
        public string Operator { get; set; }
        public string Criteria { get; set; }
        //public int? FilterGroupId { get; set; }
        //public string GroupOperator { get; set; }

        #endregion Properties

        #region Constructors

        public FilterCondition() { }

        public FilterCondition(string dataType,string dbFieldName, string filterOperator, string criteria = null)
        {
            DataType = dataType;
            DbFieldName = dbFieldName;
            Operator = filterOperator;
            Criteria = criteria;
            //FilterGroupId = filterGroupId;
            //GroupOperator = groupOperator;
        }

        #endregion Constructors
    }
}
