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
    ///     A CURI which inherits the scheme of its anchor and creates a new path by combining the paths of its anchor and its path.
    /// </summary>
    public class RelativeCURI : AbstractCURI
    {
        public static readonly string SCHEME = "relative";

        public string AbsolutePath
        {
            get
            {
                string source = WebUtility.UrlDecode(Compiler.Options.SourceCURI.Uri.AbsolutePath);
                string? dir = Path.GetDirectoryName(source);

                string uri = WebUtility.UrlDecode(Uri.AbsolutePath);

                string path = Path.Combine(dir!, uri);
                return path;
            }
        }

        public AbstractCURI Anchor { get; private set; }

        public AbstractCURI Result
        {
            get
            {
                return null;
            }
        }

        public override bool Equals (AbstractCURI? other)
        {
            return other?.Uri?.Equals(Uri) ?? false;
        }

        public RelativeCURI (Uri relative, AbstractCURI anchor) : base(relative)
        {
            Anchor = anchor;
            if (SCHEME.Equals(Anchor.Uri.Scheme)) throw new UriFormatException($"The anchor {anchor} for the {GetType()} {relative} may not be relative.");
            if (!SCHEME.Equals(relative.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {SCHEME}. Found '{relative.Scheme}'.");

            string abs = WebUtility.UrlDecode(Uri.AbsolutePath);
            if (Path.IsPathRooted(abs)) throw new UriFormatException($"The path of a {GetType()} may not be rooted.");
        }

        public override Stream GetStream ()
        {
            try
            {
                return Result.GetStream();
            }
            catch (Exception ex)
            {
                Crimson.Panic($"{GetType().Name}: An error occurred while awaiting a read operation on {AbsolutePath}.", Crimson.PanicCode.CURI_STREAM, ex);
                throw;
            }
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri uri)
            {
                return new RelativeCURI(uri);
            }
        }
    }
}
