using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi.LinkHeader
{
    /// <summary>
    /// Add a "Link" HTTP header to responses pointing to a WebAPI route.
    /// </summary>
    /// <remarks>Parameters passed to this route are passed through to the target route specified by <see cref="RouteName"/>.</remarks>
    public class RouteLinkHeaderAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The name of the WebAPI route the link shall point to.
        /// </summary>
        public string RouteName { get; }

        /// <summary>
        /// The relation type of the link.
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// A human-readable description of the link.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Controls whether route parameters are passed through from the current route to the target route specified by <see cref="RouteName"/>.
        /// </summary>
        public bool PassThroughRouteParameters { get; set; }

        /// <summary>
        /// Creates a link attribute.
        /// </summary>
        /// <param name="routeName">The name of the WebAPI route the link shall point to.</param>
        public RouteLinkHeaderAttribute(string routeName)
        {
            RouteName = routeName;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null) return;

            string href = actionExecutedContext.Request.GetUrlHelper().Link(RouteName,
                PassThroughRouteParameters ? actionExecutedContext.Request.GetRouteData().Values : null);
            actionExecutedContext.Response.Headers.AddLink(href, Rel, Title);
        }
    }
}