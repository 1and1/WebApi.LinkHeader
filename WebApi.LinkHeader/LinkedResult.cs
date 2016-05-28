using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.LinkHeader
{
    /// <summary>
    /// A wrapper around an HTTP action result that adds a Link header.
    /// </summary>
    public class LinkedResult : IHttpActionResult
    {
        private readonly IHttpActionResult _inner;
        private readonly string _href;
        private readonly string _rel;
        private readonly string _title;
        private readonly bool _templated;

        /// <summary>
        /// Creates a wrapper around an HTTP action result that adds a Link header.
        /// </summary>
        /// <param name="inner">The result to add the Link header to.</param>
        /// <param name="href">The URI the link shall point to. If it starts with a slash it is relative to the API root URI otherwise it is relative to the <see cref="HttpRequestMessage.RequestUri"/>. Trailing slashes are automatically appended to the base URI.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        /// <param name="templated"><c>true</c> if <paramref name="href"/> is an RFC 6570 URI Template; <c>false</c> if it is a normal RFC 3986 URI.</param>
        public LinkedResult(IHttpActionResult inner, string href, string rel = null,
            string title = null, bool templated = false)
        {
            _inner = inner;
            _href = href;
            _rel = rel;
            _title = title;
            _templated = templated;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await _inner.ExecuteAsync(cancellationToken);
            response.AddLink(_href, _rel, _title, _templated);
            return response;
        }
    }
}