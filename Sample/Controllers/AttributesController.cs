using System.Web.Http;

namespace WebApi.LinkHeader.Sample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of the <see cref="LinkHeaderAttribute"/>.
    /// </summary>
    [RoutePrefix("api/attributes")]
    public class AttributesController : ApiController
    {
        [HttpGet, Route(""), LinkHeader("self", ".")]
        public string Sample()
        {
            return "Hello";
        }
    }
}