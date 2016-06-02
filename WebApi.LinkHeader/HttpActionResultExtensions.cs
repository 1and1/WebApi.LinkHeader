using System.Net.Http;
using System.Web.Http;

namespace WebApi.LinkHeader
{
    public static class HttpActionResultExtensions
    {
        /// <summary>
        /// Creates a wrapper around the result that adds a Link header.
        /// </summary>
        /// <param name="result">The result to add the Link header to.</param>
        /// <param name="href">The URI the link shall point to. If it starts with a slash it is relative to the virtual path root otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes are automatically appended to the base URI.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        /// <param name="templated"><c>true</c> if <paramref name="href"/> is an RFC 6570 URI Template; <c>false</c> if it is a normal RFC 3986 URI.</param>
        public static IHttpActionResult WithLink(this IHttpActionResult result, string href, string rel = null,
            string title = null, bool templated = false)
        {
            return new LinkedResult(result, href, rel, title, templated);
        }

        /// <summary>
        /// Creates a wrapper around the result that adds a Link header pointing to WebAPI route.
        /// </summary>
        /// <param name="result">The result to add the Link header to.</param>
        /// <param name="routeName">The name of the WebAPI route the link shall point to.</param>
        /// <param name="routeValues">The route data to use for generating the URI.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        public static IHttpActionResult WithRouteLink(this IHttpActionResult result, string routeName, object routeValues = null, string rel = null,
            string title = null)
        {
            return new RouteLinkedResult(result, routeName, routeValues, rel, title);
        }
    }
}