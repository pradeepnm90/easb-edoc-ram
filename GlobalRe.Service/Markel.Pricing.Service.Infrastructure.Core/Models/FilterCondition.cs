namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class FilterCondition
    {
        #region Properties

        public string DataType { get; set; }
        public string DbFieldName { get; set; }
        public string Operator { get; set; }
        public string Criteria { get; set; }

        #endregion Properties

        #region Constructors

        public FilterCondition(string dataType, string dbFieldName, string filterOperator, string criteria = null)
        {
            DataType = dataType;
            DbFieldName = dbFieldName;
            Operator = filterOperator;
            Criteria = criteria;
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            return $"{DataType}: {DbFieldName} {Operator} {Criteria}";
        }

        #endregion
    }
}
