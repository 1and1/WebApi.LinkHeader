using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace WebApi.LinkHeader
{
    public static class HttpResponseHeadersExtensions
    {
        /// <summary>
        /// Adds a Link header.
        /// </summary>
        /// <param name="headers">The header collection to add the Link header to.</param>
        /// <param name="href">The URI the link shall point to.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        /// <param name="templated"><c>true</c> if <paramref name="href"/> is an RFC 6570 URI Template; <c>false</c> if it is a normal RFC 3986 URI.</param>
        public static void AddLink(this HttpResponseHeaders headers, Uri href, string rel = null,
            string title = null, bool templated = false)
        {
            string escaped = href.ToStringRfc();

            // Preserve curly braces unescaped for template support
            if (templated) escaped = escaped.Replace("%7B", "{").Replace("%7D", "}");

            var builder = new StringBuilder("<" + escaped + ">");
            if (!string.IsNullOrEmpty(rel)) builder.Append("; rel=" + Escape(rel));
            if (!string.IsNullOrEmpty(title)) builder.Append("; title=" + Escape(title));
            if (templated) builder.Append("; templated=true");

            headers.Add("Link", builder.ToString());
        }

        /// <summary>
        /// Escapes a string for safe use as a property value in an HTTP header field.
        /// </summary>
        private static string Escape(string value)
        {
            return value.Any(x => char.IsWhiteSpace(x) || x == ';' || x == ',')
                ? "\"" + value.Replace("\"", "\\\"") + "\""
                : value;
        }

        /// <summary>
        /// Adds a Link header.
        /// </summary>
        /// <param name="headers">The header collection to add the Link header to.</param>
        /// <param name="href">The URI the link shall point to.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        /// <param name="templated"><c>true</c> if <paramref name="href"/> is an RFC 6570 URI Template; <c>false</c> if it is a normal RFC 3986 URI.</param>
        public static void AddLink(this HttpResponseHeaders headers, string href, string rel = null,
            string title = null, bool templated = false)
        {
            headers.AddLink(new Uri(href, UriKind.RelativeOrAbsolute), rel, title, templated);
        }
    }
}
