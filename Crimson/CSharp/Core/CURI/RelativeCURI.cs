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

        public override bool Equals (AbstractCURI? other)
        {
            return other?.Uri?.Equals(Uri) ?? false;
        }

        public RelativeCURI (Uri uri) : base(uri)
        {
            if (!SCHEME.Equals(uri.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {SCHEME}. Found '{uri.Scheme}'.");

            string abs = WebUtility.UrlDecode(Uri.AbsolutePath);
            if (Path.IsPathRooted(abs)) throw new UriFormatException($"The path of a {GetType()} may not be rooted.");
        }

        public override Stream GetStream ()
        {
            try
            {
                return File.OpenRead(AbsolutePath);
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
