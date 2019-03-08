using Markel.Pricing.Service.Infrastructure.Extensions;
using System;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class DateValueConfiguration
    {
        public DateTime FromDate { get; set; }
        public string[] Values { get; set; }

        public bool Contains(string value)
        {
            return Values.Contains(value);
        }

        public override string ToString()
        {
            return $"{FromDate.ToShortDateString()}: {Values.Join(", ")}";
        }
    }
}
