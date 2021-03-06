﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.LinkHeader;

namespace LinkHeaderSample.Controllers
{
    /// <summary>
    /// Demonstrates the usage of the <see cref="LinkHeaderAttribute"/>.
    /// </summary>
    [RoutePrefix("links")]
    public class LinksController : ApiController
    {
        /// <summary>
        /// Entry point with relative links to child elements.
        /// </summary>
        [HttpGet, Route("")]
        [LinkHeader("jane", Rel = "person"), LinkHeader("john", Rel = "person"), LinkHeader("products", Rel = "products")]
        public void Overview()
        {
        }

        /// <summary>
        /// Node with absolute link to related node.
        /// </summary>
        [HttpGet, Route("jane")]
        [LinkHeader("/links/john", Rel = "husband")]
        public string Jane()
        {
            return "Jane Doe";
        }

        /// <summary>
        /// Node with relative link to related node.
        /// </summary>
        [HttpGet, Route("john")]
        [LinkHeader("../jane", Rel = "wife")]
        public string John()
        {
            return "John Doe";
        }

        /// <summary>
        /// Collection with a template link for the child elements.
        /// </summary>
        [HttpGet, Route("products")]
        [LinkHeader("{id}", Rel = "child", Templated = true)]
        public IEnumerable<int> Products()
        {
            return new[] {1, 2, 3};
        }

        /// <summary>
        /// Collection element with relative link to child element.
        /// </summary>
        [HttpGet, Route("products/{id}")]
        [LinkHeader("price", Rel = "price")]
        public string Product(int id)
        {
            return "Product #" + id;
        }

        /// <summary>
        /// Child element with no explicit links.
        /// </summary>
        [HttpGet, Route("persons/{id}/price")]
        public string ProductPrice(int id)
        {
            return id + ",- USD";
        }

        /// <summary>
        /// Always throws an exception, which causes the link header to be ignored/skipped.
        /// </summary>
        [HttpGet, Route("exception")]
        [LinkHeader("/")]
        public void Exception()
        {
            throw new Exception("Test exception");
        }
    }
}