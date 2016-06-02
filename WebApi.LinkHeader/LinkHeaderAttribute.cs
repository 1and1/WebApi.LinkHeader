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
        /// The URI the link shall point to. If it starts with a slash it is relative to the virtual path root otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes are automatically appended to the base URI.
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
        /// <c>true</c> if <see cref="Href"/> is an RFC 6570 URI Template; <c>false</c> if it is a normal RFC 3986 URI.
        /// </summary>
        public bool Templated { get; set; }

        /// <summary>
        /// Creates a link attribute.
        /// </summary>
        /// <param name="href">The URI the link shall point to. If it starts with a slash it is relative to the virtual path root otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes are automatically appended to the base URI.</param>
        public LinkHeaderAttribute(string href)
        {
            Href = href;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response
                ?.AddLink(Href, Rel, Title, Templated);
        }
    }
}