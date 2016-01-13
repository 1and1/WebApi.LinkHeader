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
        /// The URI the link shall point to. If it starts with a slash it is relative to the API root URI otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes automatically appended to the base URI.
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
        /// <param name="href">The URI the link shall point to. If it starts with a slash it is relative to the API root URI otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes automatically appended to the base URI.</param>
        public LinkHeaderAttribute(string href)
        {
            Href = href;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null) return;

            string href = Href.StartsWith("/")
                ? EnsureTrailingSlash(actionExecutedContext.Request.GetRequestContext().VirtualPathRoot) + Href.Substring(1)
                : Href;
            var uri = new Uri(EnsureTrailingSlash(actionExecutedContext.Request.RequestUri), href);

            actionExecutedContext.Response.Headers.AddLink(uri, Rel, Title);
        }

        /// <summary>
        /// Adds a trailing slash to the URI if it does not already have one.
        /// </summary>
        private static string EnsureTrailingSlash(string uri)
        {
            return uri.EndsWith("/") ? uri : uri + "/";
        }

        /// <summary>
        /// Adds a trailing slash to the URI if it does not already have one.
        /// </summary>
        private static Uri EnsureTrailingSlash(Uri uri)
        {
            return uri.OriginalString.EndsWith("/")
                ? uri
                : new Uri(uri.OriginalString + "/", uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
        }
    }
}
