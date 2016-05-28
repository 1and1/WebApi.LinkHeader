# WebApi.LinkHeader

WebApi.LinkHeader allows you to easily add [HTTP Link headers](http://www.w3.org/wiki/LinkHeader) to existing [WebAPI](http://www.asp.net/web-api) endpoints.

NuGet package:
* [WebApi.LinkHeader](https://www.nuget.org/packages/WebApi.LinkHeader/)



## Usage

Simply add the `[LinkHeader("URI")]` attribute to a controller method.

Sample:
```cs
[LinkHeader("http://domain/path", Rel = "parent", Title = "Parent")]
public Resource Get()
{
  // ...
}
```


### Relative links

You can also use relative links. Relative URIs are resolved server-side and sent to the client as absolute URIs in the Link header.

Trailing slashes are implicitly added to request URIs before resolving relative URIs. For example in this case both requests for `http://yourservice/person/1` and `http://yourservice/person/1/` will produce a link to `http://yourservice/person/1/address`:
```cs
[HttpGet, Route("/person/{id}")]
[LinkHeader("address", Rel = "address")]
public Person Person(int id)
{
  // ...
}

[HttpGet, Route("/people/{id}/address")]
public Address Address(int id)
{
  // ...
}
```

Links starting with `/` are treated as relative to the root of the WebAPI application, not the domain.


### Link templates

[URI Templates](https://tools.ietf.org/html/rfc6570) are URIs with placeholders like `{id}`. You can use the attribute property `Templated` to indicate the Href should be treated as a template.

This adds the property `templated=true` to the HTTP Link Header. It also prevents `{` and `}` from being escaped using URL encoding, e.g. `{id}` does not get turned into `%7Bid%7D`.

Sample:
```cs
[LinkHeader("http://domain/path/{id}", Rel = "child", Templated = true)]
public Resource Get()
{
  // ...
}
```


### Route links

You can use `[RouteLinkHeader("RouteName")]` instead of `[LinkHeader("URI")]` to generate links that point to WebAPI routes by name.

Parameters passed to the annotated target are passed through to the target route. For example in this case requests for `http://yourservice/person/1` will produce a link to `http://yourservice/address/1`:

Sample:
```cs
[HttpGet, Route("/person/{id}")]
[RouteLinkHeader("Address", Rel = "address")]
public Person Person(int id)
{
  // ...
}

[HttpGet, Route("/people/{id}/address", Name = "Address")]
public Address Address(int id)
{
  // ...
}
```


### Programatically generated links

You can use the `.AddLink()` extension method for `HttpResponseMessage` to set programatically generated links.

Sample:
```cs
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
    response.AddLink(
      "products/" + productId,
      rel: "product",
      title: "Product #" + productId);
  }
  return response;
}

/// <summary>
/// Collection element with relative link to child element.
/// </summary>
[HttpGet, Route("products/{id}", Name = "Product")]
public string Product(int id)
{
  // ...
}
```


## Sample project

The source code includes a sample project that uses demonstrates the usage of WebApi.LinkHeader. You can build and run it using Visual Studio 2015. By default the instance will be hosted by IIS Express at `http://localhost:42980/sample/links/`, `http://localhost:42980/sample/route-links/` and `http://localhost:42980/sample/programatic-links/`.
