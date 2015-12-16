using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.LinkHeader.Sample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of the <see cref="RouteLinkHeaderAttribute"/>.
    /// </summary>
    [RoutePrefix("api/route-links")]
    public class RouteLinksController : ApiController
    {
        [HttpGet, Route("")]
        [RouteLinkHeader("Jane", Rel = "person"), RouteLinkHeader("John", Rel = "person"), RouteLinkHeader("Products", Rel = "persons")]
        public void Overview()
        {
        }

        [HttpGet, Route("jane", Name = "Jane")]
        [RouteLinkHeader("John", Rel = "husband")]
        public string Jane()
        {
            return "Jane Doe";
        }

        [HttpGet, Route("john", Name = "John")]
        [RouteLinkHeader("Jane", Rel = "wife")]
        public string John()
        {
            return "John Doe";
        }

        [HttpGet, Route("products", Name = "Products")]
        public IEnumerable<int> Products()
        {
            return new[] {1, 2, 3};
        }

        [HttpGet, Route("products/{id}")]
        [RouteLinkHeader("ProductPrice", Rel = "price", PassThroughRouteParameters = true)]
        public string Product(int id)
        {
            return "Prodcut #" + id;
        }

        [HttpGet, Route("persons/{id}/price", Name = "ProductPrice")]
        public string ProductPrice(int id)
        {
            return id + ",- USD";
        }
    }
}