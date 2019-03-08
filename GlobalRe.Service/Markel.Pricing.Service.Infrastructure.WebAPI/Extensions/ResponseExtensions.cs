using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class ResponseExtensions
    {
        #region API Links

        /// <summary>
        /// Reference Entities contain reference data required for presenting entity options to a user.
        /// Eg. list of external system types
        /// </summary>
        /// <param name="links">List of Links</param>
        /// <param name="rel">Entity Name</param>
        /// <param name="href">Relative URL to the resource</param>
        /// <param name="mthodType">HTTP Method Type</param>
        public static void AddReferenceEntity(this List<Link> links, string rel, string href, HttpMethodType mthodType = HttpMethodType.GET, string criteria = null)
        {
            string link = string.IsNullOrEmpty(criteria) ? href : $"{href}?{criteria}";

            links.Add(new Link(LinkType.ReferenceEntity, rel, link, mthodType));
        }

        public static void AddEntity(this List<Link> links, string rel, string href, HttpMethodType mthodType = HttpMethodType.GET)
        {
            links.Add(new Link(LinkType.Entity, rel, href, mthodType));
        }

        public static void AddRelatedEntity(this List<Link> links, string rel, string href, HttpMethodType mthodType = HttpMethodType.GET)
        {
            links.Add(new Link(LinkType.RelatedEntity, rel, href, mthodType));
        }

        public static void AddSubEntity(this List<Link> links, string rel, string href, HttpMethodType mthodType = HttpMethodType.GET)
        {
            links.Add(new Link(LinkType.SubEntity, rel, href, mthodType));
        }

        public static void AddAction(this List<Link> links, string rel, string href, HttpMethodType mthodType)
        {
            links.Add(new Link(LinkType.Action, rel, href, mthodType));
        }

        public static void Add(this List<Link> links, string rel, string href, EntityActionType entityAction)
        {
            switch (entityAction)
            {
                case EntityActionType.ReferenceEntity:
                    links.AddReferenceEntity(rel, href);
                    break;

                case EntityActionType.Entity:
                    //links.AddEntity(rel, href, HttpMethodType.OPTIONS);
                    links.AddEntity(rel, href);
                    break;

                case EntityActionType.EntitySearch:
                    //links.AddEntity(rel, href.Replace("/search", ""), HttpMethodType.OPTIONS);
                    links.AddEntity(rel, href, HttpMethodType.POST);
                    break;

                case EntityActionType.RelatedEntity:
                    //links.AddRelatedEntity(rel, href, HttpMethodType.OPTIONS);
                    links.AddRelatedEntity(rel, href);
                    break;

                case EntityActionType.SubEntity:
                    //links.AddSubEntity(rel, href, HttpMethodType.OPTIONS);
                    links.AddSubEntity(rel, href);
                    break;

                case EntityActionType.SubEntitySearch:
                    //links.AddSubEntity(rel, href.Replace("/search", ""), HttpMethodType.OPTIONS);
                    links.AddSubEntity(rel, href, HttpMethodType.POST);
                    break;

                case EntityActionType.Action_Add:
                    links.AddAction(rel, href, HttpMethodType.POST);
                    links.AddAction(rel, href, HttpMethodType.OPTIONS); // TODO: This line should be removed and the options on all the other gets should be uncommented
                    break;

                case EntityActionType.Action_Update:
                    links.AddAction(rel, href, HttpMethodType.PUT);
                    break;

                case EntityActionType.Action_Partial_Update:
                    links.AddAction(rel, href, HttpMethodType.PATCH);
                    break;

                case EntityActionType.Action_Delete:
                    links.AddAction(rel, href, HttpMethodType.DELETE);
                    break;

                case EntityActionType.Action_Clone:
                    links.AddAction(rel, href + "/clone", HttpMethodType.POST);
                    break;

                default:
                    throw new Exception(string.Format("Unhandled Entity Action Type '{0}'", rel));
            }
        }

        #endregion API Links

        #region Header Manipulation

        private static string REQUEST_ID_HEADER = "X-Request-ID";
        public static void ApplyRequestIDHeader(this HttpResponseMessage response, HttpRequestMessage request)
        {
            IEnumerable<string> requestIDs;
            if (response != null && request.Headers.TryGetValues(REQUEST_ID_HEADER, out requestIDs))
            {
                foreach (string requestId in requestIDs)
                {
                    response.Headers.Add(REQUEST_ID_HEADER, requestId);
                }
            }
        }

        #endregion Header Manipulation
    }
}
