using CrimsonCore.Exceptions;
using CrimsonCore.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonCore.CURI
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
    public class NativeCURI : AbstractCURI
    {

        public static readonly string SCHEME = "native";

        public string AbsolutePath
        {
            get
            {
                string native = WebUtility.UrlDecode(Compiler.Options?.NativeCURI.Uri.AbsolutePath);

                string abs = WebUtility.UrlDecode(Uri.AbsolutePath);

                string path = Path.Combine(native, abs);
                return path;
            }
        }

        public NativeCURI (Uri uri) : base(uri)
        {
            if (!SCHEME.Equals(uri.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {SCHEME}. Found '{uri.Scheme}'.");
            if (Path.IsPathRooted(AbsolutePath)) throw new UriFormatException($"The path of a URI with host {SCHEME} may not be rooted.");
        }

        public override bool Equals (AbstractCURI? other)
        {
            return other?.Uri?.Equals(Uri) ?? false;
        }

        public override Stream GetStream ()
        {
            try
            {
                return File.OpenRead(AbsolutePath);
            }
            catch (Exception ex)
            {
                CrimsonCore.Panic($"{GetType().Name}: An error occurred while awaiting a read operation on {AbsolutePath}.", CrimsonCore.PanicCode.CURI_STREAM, ex);
                throw;
            }
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri uri)
            {
                return new NativeCURI(uri);
            }

            public AbstractCURI Make (Uri relativeOrAbsoluteUri, AbstractCURI? anchor)
            {
                throw new NotImplementedException();
            }
        }
    }
}
