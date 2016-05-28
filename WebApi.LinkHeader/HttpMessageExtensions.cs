using System;
using System.Net.Http;

namespace WebApi.LinkHeader
{
    public static class HttpMessageExtensions
    {
        /// <summary>
        /// Adds a Link header to an HTTP response message.
        /// </summary>
        /// <param name="response">The message to add the Link header to.</param>
        /// <param name="href">The URI the link shall point to. If it starts with a slash it is relative to the API root URI otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes are automatically appended to the base URI.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        /// <param name="templated"><c>true</c> if <paramref name="href"/> is an RFC 6570 URI Template; <c>false</c> if it is a normal RFC 3986 URI.</param>
        public static void AddLink(this HttpResponseMessage response, string href, string rel = null,
            string title = null, bool templated = false)
        {
            response.Headers.AddLink(response.RequestMessage.RelativeLink(href), rel, title, templated);
        }

        /// <summary>
        /// Resolves a relative link.
        /// If it starts with a slash it is relative to the API root URI otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>.
        /// Trailing slashes are automatically appended to the base URI.
        /// </summary>
        public static Uri RelativeLink(this HttpRequestMessage request, string href)
        {
            return href.StartsWith("/")
                ? new Uri(EnsureTrailingSlash(request.GetRequestContext().VirtualPathRoot) + href.Substring(1))
                : new Uri(request.RequestUri.EnsureTrailingSlash(), href);
        }

        /// <summary>
        /// Adds a trailing slash to the URI if it does not already have one.
        /// </summary>
        private static string EnsureTrailingSlash(string uri)
        {
            return uri.EndsWith("/") ? uri : uri + "/";
        }
    }
}