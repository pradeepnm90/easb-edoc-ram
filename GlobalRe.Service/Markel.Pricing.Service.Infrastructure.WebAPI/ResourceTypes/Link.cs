using System;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public class Link
    {
        public string type { private set; get; }
        public string rel { private set; get; }
        public string href { private set; get; }
        public string method { private set; get; }

        public Link(LinkType type, Enum rel, string href, HttpMethodType method) :
            this(
                type: type,
                rel: rel.ToString(),
                href: href,
                method: method
            )
        { }

        public Link(LinkType type, string rel, string href, HttpMethodType method)
        {
            this.type = type.ToString();
            this.rel = rel.ToString();
            this.href = href;
            this.method = method.ToString();
        }

        public override string ToString()
        {
            return string.Format("'type': '{0}', 'rel': '{1}', 'method': '{2}', 'href': '{3}'", type, rel, method, href);
        }
    }
}