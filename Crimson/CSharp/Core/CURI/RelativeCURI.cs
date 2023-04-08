using Crimson.CSharp.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core.CURI
{
    /// <summary>
    /// <para>
    ///     <b>Crimson URI</b>
    /// </para>
    /// <para>
    ///     A wrapper class for a Uri which conceals all of the strange things which Crimson 
    ///     does in the background (i.e. custom "root.crimson" and "native.crimson" hosts).
    /// </para>
    /// </summary>
    public abstract class RelativeCURI : AbstractCURI
    {
        public static readonly string SCHEME = "relative";
        public RelativeCURI (Uri uri) : base(uri)
        {
            if (!Uri.UriSchemeHttp.Equals(uri.Scheme))
            {
                throw new UriFormatException("The");
            }
        }

        public override async Task<Stream> GetStream ()
        {
            if (Path.IsPathRooted(uriPath)) throw new UriFormatException($"The path of a URI with host {RELATIVE_SCHEME} may not be rooted.");
            string source = WebUtility.UrlDecode(Crimson.Options.SourceUri.AbsolutePath);
            string dir = Path.GetDirectoryName(source)!;
            return Path.Combine(dir, uriPath);
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri uri)
            {
                return null;
            }
        }
    }
}
