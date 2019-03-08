using Markel.Pricing.Service.Infrastructure.Constants;
using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    [Serializable]
    public class AggregateField
    {
        [DataMember]
        public string Field { get; set; }
        
        [DataMember]
        public AggregateFunctions AggregateFunction { get; set; }

        public AggregateField() { }

        public AggregateField(string field, AggregateFunctions aggregateFunction)
        {
            this.Field = field;
            this.AggregateFunction = aggregateFunction;           
        }

    }
}
