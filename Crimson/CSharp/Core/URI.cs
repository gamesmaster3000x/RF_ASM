using Crimson.CSharp.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// A wrapper class for a Uri which conceals all of the strange things which Crimson 
    /// does in the background (i.e. custom "root.crimson" and "native.crimson" hosts).
    /// </summary>
    public class URI
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static object?[] AcceptedSchemes { get; set; } = new string[]
        {
            Uri.UriSchemeFile, Uri.UriSchemeHttp
        };
        public static readonly string NATIVE_HOST = "native.crimson";
        public static readonly string ROOT_HOST = "root.crimson";
        public static readonly string ABSOLUTE_HOST = "abs.crimson";
        public static string[] CustomHosts { get; set; } = new string[]
        {
            NATIVE_HOST, ROOT_HOST, ABSOLUTE_HOST
        };

        public Uri Uri { get; set; }

        public URI (string uriText)
        {
            string trimmedText = uriText.Trim(' ', '\t', '\n', '\v', '\f', '\r', '"');

            Uri? uri;
            if (!Uri.TryCreate(trimmedText, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = false }, out uri))
            {
                throw new UriFormatException($"Unable to parse illegal URI string '{trimmedText}' ({uriText})");
            }
            Uri = StandardiseUri(uri);
        }

        public URI (Uri uri)
        {
            Uri = StandardiseUri(uri);
        }

        // ================

        /// <summary>
        /// Removes any annoying crimson witchcraft from the URI and converts it to something which could just be put into a browser.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="UriFormatException"></exception>
        private Uri StandardiseUri (Uri uri)
        {
            if (uri.Scheme == Uri.UriSchemeFile)
                return StandardiseFileUri(uri);
            else if (uri.Scheme == Uri.UriSchemeHttp)
                return StandardiseHttpUri(uri);

            throw new UriFormatException($"Crimson only accepts URIs of the file:/// scheme at this time. Found: {uri.Scheme}");
        }

        /// <summary>
        /// file://root.crimson/memory/heap.crm
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private Uri StandardiseFileUri (Uri uri)
        {
            UriBuilder builder = new UriBuilder(uri);

            if (String.IsNullOrWhiteSpace(builder.Host))
            {
                builder.Host = ABSOLUTE_HOST;
            }

            // file:///native.crimson/heap.crm
            if (builder.Host.Equals(NATIVE_HOST))
            {
                builder.Path = $"{Crimson.Options.NativeUri.AbsolutePath}/{builder.Path}";
            }

            // file://root.crimson/heap.crm
            if (builder.Host.Equals(ROOT_HOST))
            {
                string srcPath = URIs.GetAbsolutePath(Crimson.Options.SourceUri);
                string? parentDirectory = Path.GetDirectoryName(srcPath);
                builder.Path = $"{parentDirectory}{builder.Path}";
            }

            return builder.Uri;
        }

        /// <summary>
        /// http://example.com/path/to/thing.crm
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private Uri StandardiseHttpUri (Uri uri)
        {
            return uri;
        }



        /// <summary>
        /// file://
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string GetAbsolutePath (Uri uri)
        {
            if (uri.Scheme != Uri.UriSchemeFile)
                throw new UriSchemeException(uri);

            string trimmed = uri.AbsolutePath.TrimStart('/', '\\', '\n', '\t');
            string uriPath = WebUtility.UrlDecode(trimmed);

            // file://abs.crimson/C:/etc/thing.crm
            // C://etc/thing.crm
            if (ABSOLUTE_HOST.Equals(uri.Host))
            {
                return uriPath;
            }

            if (NATIVE_HOST.Equals(uri.Host))
            {
                if (Path.IsPathRooted(uriPath)) throw new UriFormatException($"The path of a URI with host {NATIVE_HOST} may not be rooted.");
                string native = WebUtility.UrlDecode(Crimson.Options.NativeUri.AbsolutePath);
                return Path.Combine(native, uriPath);
            }
            else if (ROOT_HOST.Equals(uri.Host))
            {
                if (Path.IsPathRooted(uriPath)) throw new UriFormatException($"The path of a URI with host {ROOT_HOST} may not be rooted.");
                string source = WebUtility.UrlDecode(Crimson.Options.SourceUri.AbsolutePath);
                string dir = Path.GetDirectoryName(source)!;
                return Path.Combine(dir, uriPath);
            }

            return "URIS GET ABSOLUTE PATH TESTING";
        }
    }
}
