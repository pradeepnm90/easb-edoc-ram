using Markel.Pricing.Service.Infrastructure.Constants;
using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    [Serializable]
    public class FilterParameter
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public object Value { get; set; }
        [DataMember]
        public ComparisonType FilterOperator { get; set; }
        [DataMember]
        public ConditionType GroupOperator { get; set; }
        [DataMember]
        public int? Group { get; set; }

        public FilterParameter() { }

        public FilterParameter(string name, object value) : this(name, ComparisonType.Equals, value) { }

        public FilterParameter(string name, ComparisonType filterOperator, object value, ConditionType groupOperator = ConditionType.And, int group = 0)
        {
            this.Name = name;
            this.Value = value;
            this.FilterOperator = filterOperator;
            this.GroupOperator = groupOperator;
            this.Group = group;
        }

        public override string ToString()
        {
            return $"[{Group ?? 0}] ({Name} {FilterOperator} '{Value}') {GroupOperator}";
        }
    }
}
