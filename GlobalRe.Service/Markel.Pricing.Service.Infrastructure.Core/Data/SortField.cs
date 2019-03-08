namespace Markel.Pricing.Service.Infrastructure.Data
{
    public class SortField
    {
        public string FieldName { get; set; }
        public SortOrderType SortOrder { get; set; } = SortOrderType.None;

        public override string ToString()
        {
            return string.Format("'FieldName': '{0}', 'SortOrder': '{1}'", FieldName, SortOrder);
        }
    }
}
