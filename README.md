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
[Route("/person/{id}"), LinkHeader("address")]
public Person Person(int id)
{
  // ...
}

[Route("/people/{id}/address")]
public Address Address(int id)
{
  // ...
}
```

Links starting with `/` are treated as relative to the root of the WebAPI application, not the domain.


### Route links

You can use `[RouteLinkHeader("RouteName")]` instead of `[LinkHeader("URI")]` to generate links that point to WebAPI routes by name.

Parameters passed to the annotated target are passed through to the target route. For example in this case requests for `http://yourservice/person/1` will produce a link to `http://yourservice/address/1`:

Sample:
```cs
[Route("/person/{id}"), RouteLinkHeader("Address")]
public Person Person(int id)
{
  // ...
}

[Route("/address/{id}", Name = "Address")]
public Address Address(int id)
{
  // ...
}
```


## Sample project

The source code includes a sample project that uses demonstrates the usage of WebApi.LinkHeader. You can build and run it using Visual Studio 2015. By default the instance will be hosted by IIS Express at `http://localhost:42980/sample/links/` and `http://localhost:42980/sample/route-links/`.
