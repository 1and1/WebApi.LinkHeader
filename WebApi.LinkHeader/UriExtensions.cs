using System;

namespace WebApi.LinkHeader
{
    public static class UriExtensions
    {
        /// <summary>
        /// Adds a trailing slash to the URI if it does not already have one.
        /// </summary>
        public static Uri EnsureTrailingSlash(this Uri uri)
        {
            return uri.OriginalString.EndsWith("/")
                ? uri
                : new Uri(uri.OriginalString + "/", uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
        }

        /// <summary>
        /// An alternate version of <see cref="Uri.ToString"/> that produces results escaped according to RFC 2396.
        /// </summary>
        public static string ToStringRfc(this Uri uri)
        {
            return uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString;
        }
    }
}