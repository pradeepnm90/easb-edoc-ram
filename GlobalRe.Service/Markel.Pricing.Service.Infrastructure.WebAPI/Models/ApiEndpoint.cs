using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class ApiEndpoint
    {
        public string RouteTemplate { get; private set; }
        public IList<string> Methods { get; private set; }

        public ApiEndpoint(string routeTemplate)
        {
            this.RouteTemplate = routeTemplate;

            this.Methods = new List<string>();
        }

        public void AddMethod(string method)
        {
            Methods.Add(method);
        }

        public override string ToString()
        {
            return $"{ this.RouteTemplate } [{ this.Methods.Aggregate((i, j) => i + "," + j) }]";
        }
    }
}
