using System;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace WebApi.LinkHeader
{
    [TestFixture]
    public class HttpResponseHeaderExtensionsTest
    {
        [Test]
        public void TestAddLink()
        {
            var headers = new HttpResponseMessage().Headers;
            headers.AddLink(new Uri("http://myuri/123"), rel: "my-rel", title: "Title");

            string linkHeader = headers.GetValues("Link").First();
            linkHeader.Should().Be("<http://myuri/123>; rel=my-rel; title=Title");
        }

        [Test]
        public void TestAddLinkEscaping()
        {
            var headers = new HttpResponseMessage().Headers;
            headers.AddLink(new Uri("http://myuri/{id}"), rel: "my;rel", title: "My Title");

            string linkHeader = headers.GetValues("Link").First();
            linkHeader.Should().Be("<http://myuri/%7Bid%7D>; rel=\"my;rel\"; title=\"My Title\"");
        }

        [Test]
        public void TestAddLinkTemplate()
        {
            var headers = new HttpResponseMessage().Headers;
            headers.AddLink(new Uri("http://myuri/{id}"), rel: "my-rel", templated: true);

            string linkHeader = headers.GetValues("Link").First();
            linkHeader.Should().Be("<http://myuri/{id}>; rel=my-rel; templated=true");
        }
    }
}