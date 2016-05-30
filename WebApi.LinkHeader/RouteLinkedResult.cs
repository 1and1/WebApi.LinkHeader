using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.LinkHeader
{
    /// <summary>
    /// A wrapper around an HTTP action result that adds a route-based Link header.
    /// </summary>
    public class RouteLinkedResult : IHttpActionResult
    {
        private readonly IHttpActionResult _inner;
        private readonly string _routeName;
        private readonly object _routeValues;
        private readonly string _rel;
        private readonly string _title;

        /// <summary>
        /// Creates a wrapper around an HTTP action result that adds a Link header.
        /// </summary>
        /// <param name="inner">The result to add the Link header to.</param>
        /// <param name="routeName">The name of the WebAPI route the link shall point to.</param>
        /// <param name="routeValues">The route data to use for generating the URI.</param>
        /// <param name="rel">The relation type of the link.</param>
        /// <param name="title">A human-readable description of the link.</param>
        public RouteLinkedResult(IHttpActionResult inner, string routeName, object routeValues = null, string rel = null,
            string title = null)
        {
            _inner = inner;
            _routeName = routeName;
            _routeValues = routeValues;
            _rel = rel;
            _title = title;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await _inner.ExecuteAsync(cancellationToken);
            response.AddRouteLink(_routeName, _routeValues, _rel, _title);
            return response;
        }
    }
}