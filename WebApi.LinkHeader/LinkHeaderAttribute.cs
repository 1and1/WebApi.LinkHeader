using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi.LinkHeader
{
    /// <summary>
    /// Add a "Link" HTTP header to responses.
    /// </summary>
    public class LinkHeaderAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The URI the link shall point to, relative to the <see cref="HttpRequestMessage.RequestUri"/> (with missing trailing slashes automatically appended).
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// The relation type of the link.
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// A human-readable description of the link.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Creates a link attribute.
        /// </summary>
        /// <param name="href">The URI the link shall point to, relative to the <see cref="HttpRequestMessage.RequestUri"/> (with missing trailing slashes automatically appended).</param>
        public LinkHeaderAttribute(string href)
        {
            Href = href;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var baseUri = actionExecutedContext.Request.RequestUri.OriginalString.EndsWith("/")
                ? actionExecutedContext.Request.RequestUri
                : new Uri(actionExecutedContext.Request.RequestUri.OriginalString + "/", UriKind.Absolute);
            actionExecutedContext.Response.Headers.AddLink(new Uri(baseUri, Href), Rel, Title);
        }
    }
}
