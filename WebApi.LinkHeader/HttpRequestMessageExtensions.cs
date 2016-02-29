using System;
using System.Net.Http;

namespace WebApi.LinkHeader
{
    public static class HttpRequestMessageExtensions
    {
        public static Uri RelativeLink(this HttpRequestMessage message, string relativePath)
        {
            return new Uri(message.RequestUri.EnsureTrailingSlash(), relativePath);
        }
    }
}