using System;
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
        public static void AddLink(this HttpResponseHeaders headers, Uri href, string rel = null,
            string title = null)
        {
            if (rel != null && (rel.Contains(",") || rel.Contains(";"))) throw new ArgumentException("rel may not contain commas or semicolons because they are used as separator characters in HTTP headers.", nameof(rel));
            if (title != null && (title.Contains(",") || title.Contains(";"))) throw new ArgumentException("title may not contain commas or semicolons because they are used as separator characters in HTTP headers.", nameof(title));

            string escaped = href.AbsoluteUri.
                // Leave curly braces unescaped for template support
                Replace("%7B", "{").Replace("%7D", "}");
            var builder = new StringBuilder("<" + escaped + ">");
            if (!string.IsNullOrEmpty(rel)) builder.Append("; rel=" + rel);
            if (!string.IsNullOrEmpty(title)) builder.Append("; title=" + title);

            headers.Add("Link", builder.ToString());
        }
    }
}
