using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.ResourceTypes
{
    public enum HttpMethodType
    {
        [Description("Requests data from a specified resource")]
        GET,
        [Description("Uploads a representation of the specified URI")]
        PUT,
        [Description("Submits data to be processed to a specified resource")]
        POST,
        [Description("Deletes the specified resource")]
        DELETE,
        [Description("Returns the HTTP methods that the server supports")]
        OPTIONS,
        [Description("Updates a partial representation of the specified URI")]
        PATCH
    }
}
