namespace Markel.Pricing.Service.Infrastructure.Data
{
    public class Parameter
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
        public Parameter(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value.ToString();
        }

        public override string ToString()
        {
            return string.Format("'ParameterName': '{0}', 'Value': '{1}'", ParameterName, Value);
        }
    }
}
