# WebApi.LinkHeader

WebApi.LinkHeader allows you to easily add [HTTP Link headers](http://www.w3.org/wiki/LinkHeader) to existing [WebAPI](http://www.asp.net/web-api) endpoints.

NuGet package:
* [WebApi.LinkHeader](https://www.nuget.org/packages/WebApi.LinkHeader/)


## Getting started

1. Add the NuGet package to your project.
2. Add the `[LinkHeader("...")]` or `[RouteLinkHeader("...")]` attribute to endpoint methods.


## Sample project

The source code includes a sample project that uses demonstrates the usage of WebApi.LinkHeader. You can build and run it using Visual Studio 2015. By default the instance will be hosted by IIS Express at `http://localhost:42980/sample/`.
