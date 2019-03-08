namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class EntityChange
    {
        public string Operator { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }

        public EntityChange() { }

        public EntityChange(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", PropertyName, Value);
        }
    }
}
