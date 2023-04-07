using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core
{
    internal class URIs
    {

        public static string[] CustomHosts { get => new string[] { NATIVE_HOST, ROOT_HOST, ABSOLUTE_HOST }; }
        public static readonly string NATIVE_HOST = "native.crimson";
        public static readonly string ROOT_HOST = "root.crimson";
        public static readonly string ABSOLUTE_HOST = "abs.crimson";


        public static Uri CreateUri (string uriText)
        {
            string trimmedText = uriText.Trim(' ', '\t', '\n', '\v', '\f', '\r', '"');

            Uri? uri;
            if (!Uri.TryCreate(trimmedText, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = false }, out uri))
            {
                throw new UriFormatException($"Unable to parse illegal URI '{trimmedText}' ({uriText})");
            }

            return StandardiseUri(uri);
        }

        /// <summary>
        /// Takes a URI such as file://native.crimson/memory/heap.crm, takes the path and 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="UriFormatException"></exception>
        public static Uri StandardiseUri (Uri uri)
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
        public static Uri StandardiseFileUri (Uri uri)
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
                string? parentDirectory = Path.GetDirectoryName(Crimson.Options.SourceUri.AbsolutePath);
                builder.Path = $"{parentDirectory}/{builder.Path}";
            }

            return builder.Uri;
        }

        /// <summary>
        /// http://example.com/path/to/thing.crm
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Uri StandardiseHttpUri (Uri uri)
        {
            return uri;
        }
    }
}
