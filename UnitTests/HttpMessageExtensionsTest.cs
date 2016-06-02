using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using FluentAssertions;
using NUnit.Framework;

namespace WebApi.LinkHeader
{
    [TestFixture]
    public class HttpMessageExtensionsTest
    {
        [Test]
        public void TestAddLink()
        {
            var response = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/endpoint")
                }
            };
            response.AddLink("http://myuri/123", rel: "my-rel", title: "Title");

            string linkHeader = response.Headers.GetValues("Link").First();
            linkHeader.Should().Be("<http://myuri/123>; rel=my-rel; title=Title");
        }

        [Test]
        public void TestRelativeLink()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/endpoint")
            };
            request.RelativeLink("sub").Should().Be(new Uri("http://localhost/api/endpoint/sub"));
        }

        [Test]
        public void TestRelativeLinkRoot()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/endpoint"),
                Properties =
                {
                    [HttpPropertyKeys.RequestContextKey] = new HttpRequestContext
                    {
                        VirtualPathRoot = "/api"
                    }
                }
            };
            request.RelativeLink("/sub").Should().Be(new Uri("http://localhost/api/sub"));
        }
    }
}