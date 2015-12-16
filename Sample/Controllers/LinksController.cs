using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.LinkHeader.Sample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of the <see cref="LinkHeaderAttribute"/>.
    /// </summary>
    [RoutePrefix("api/links")]
    public class LinksController : ApiController
    {
        [HttpGet, Route("")]
        [LinkHeader("jane", Rel = "person"), LinkHeader("john", Rel = "person"), LinkHeader("products", Rel = "products")]
        public void Overview()
        {
        }

        [HttpGet, Route("jane")]
        [LinkHeader("../john", Rel = "husband")]
        public string Jane()
        {
            return "Jane Doe";
        }

        [HttpGet, Route("john")]
        [LinkHeader("../jane", Rel = "wife")]
        public string John()
        {
            return "John Doe";
        }

        [HttpGet, Route("products")]
        public IEnumerable<int> Products()
        {
            return new[] {1, 2, 3};
        }

        [HttpGet, Route("products/{id}")]
        [LinkHeader("price", Rel = "price")]
        public string Product(int id)
        {
            return "Prodcut #" + id;
        }

        [HttpGet, Route("persons/{id}/price")]
        public string ProductPrice(int id)
        {
            return id + ",- USD";
        }
    }
}