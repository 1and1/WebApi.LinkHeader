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
        private readonly string _rel;
        private readonly string _uri;

        /// <summary>
        /// Creates a link attribute.
        /// </summary>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="uri">The URI the link shall point to, relative to the <see cref="HttpRequestMessage.RequestUri"/> (with missing trailing slashes automatically appended).</param>
        public LinkHeaderAttribute(string rel, string uri)
        {
            _rel = rel;
            _uri = uri;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var baseUri = actionExecutedContext.Request.RequestUri.OriginalString.EndsWith("/")
                ? actionExecutedContext.Request.RequestUri
                : new Uri(actionExecutedContext.Request.RequestUri.OriginalString + "/", UriKind.Absolute);
            actionExecutedContext.Response.Headers.Add("Link", $"<{new Uri(baseUri, _uri).AbsoluteUri}>; rel={_rel}");
        }
    }
}
