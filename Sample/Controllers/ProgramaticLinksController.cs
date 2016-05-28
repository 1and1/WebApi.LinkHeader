using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.LinkHeader;

namespace LinkHeaderSample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of <see cref="HttpMessageExtensions.AddLink"/>.
    /// </summary>
    [RoutePrefix("programatic-links")]
    public class ProgramaticLinksController : ApiController
    {
        /// <summary>
        /// Entry point with relative links to child elements.
        /// </summary>
        [HttpGet, Route("")]
        public HttpResponseMessage Overview()
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);

            response.Headers.AddLink(
                Request.RelativeLink("products"),
                rel: "products");
            return response;
        }

        /// <summary>
        /// Collection with a template link for the child elements.
        /// </summary>
        [HttpGet, Route("products")]
        [ResponseType(typeof(IEnumerable<int>))]
        public HttpResponseMessage Products()
        {
            var productIds = new[] {1, 2, 3};
            var response = Request.CreateResponse(HttpStatusCode.OK, productIds);

            foreach (var productId in productIds)
            {
                response.AddLink(
                    Url.Link("Product", new {id = productId}),
                    rel: "product",
                    title: "Product #" + productId);
                // -or-
                //response.AddLink(
                //    "products/" + productId,
                //    rel: "product",
                //    title: "Product #" + productId);
            }
            return response;
        }

        /// <summary>
        /// Collection element with relative link to child element.
        /// </summary>
        [HttpGet, Route("products/{id}", Name = "Product")]
        public string Product(int id)
        {
            return "Product #" + id;
        }
    }
}