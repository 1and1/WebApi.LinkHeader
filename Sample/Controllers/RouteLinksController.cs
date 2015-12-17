using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.LinkHeader.Sample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of the <see cref="RouteLinkHeaderAttribute"/>.
    /// </summary>
    [RoutePrefix("route-links")]
    public class RouteLinksController : ApiController
    {
        /// <summary>
        /// Entry point with links to named routes.
        /// </summary>
        [HttpGet, Route("")]
        [RouteLinkHeader("Jane", Rel = "person"), RouteLinkHeader("John", Rel = "person"), RouteLinkHeader("Products", Rel = "persons")]
        public void Overview()
        {
        }

        /// <summary>
        /// Named node with link to related named node.
        /// </summary>
        [HttpGet, Route("jane", Name = "Jane")]
        [RouteLinkHeader("John", Rel = "husband")]
        public string Jane()
        {
            return "Jane Doe";
        }

        /// <summary>
        /// Named node with link to related named node.
        /// </summary>
        [HttpGet, Route("john", Name = "John")]
        [RouteLinkHeader("Jane", Rel = "wife")]
        public string John()
        {
            return "John Doe";
        }

        /// <summary>
        /// Named collection with no explicit links.
        /// </summary>
        [HttpGet, Route("products")]
        public IEnumerable<int> Products()
        {
            return new[] {1, 2, 3};
        }

        /// <summary>
        /// Named collection element (parameterized) with link to child route.
        /// </summary>
        [HttpGet, Route("products/{id}")]
        [RouteLinkHeader("ProductPrice", Rel = "price", PassThroughRouteParameters = true)]
        public string Product(int id)
        {
            return "Prodcut #" + id;
        }

        /// <summary>
        /// Named child element (parameterized) with no explicit links.
        /// </summary>
        [HttpGet, Route("persons/{id}/price", Name = "ProductPrice")]
        public string ProductPrice(int id)
        {
            return id + ",- USD";
        }
    }
}