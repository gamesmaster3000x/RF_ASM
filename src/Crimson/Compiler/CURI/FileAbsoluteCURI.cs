using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.CURI
{
    internal class FileAbsoluteCURI : AbstractCURI
    {
        public static readonly string SCHEME = "file";

        public string AbsolutePath
        {
            get
            {
                string path = WebUtility.UrlDecode(Uri.AbsolutePath);
                return path;
            }
        }

        public FileAbsoluteCURI (Uri uri) : base(uri)
        {
            if (!Uri.UriSchemeFile.Equals(uri.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {Uri.UriSchemeFile}. Found '{uri.Scheme}'.");
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
                CrimsonCore.Panic($"{GetType().Name}: An error occurred getting the contents of {AbsolutePath}.", CrimsonCore.PanicCode.CURI_STREAM, ex);
                throw;
            }
        }

        public class Factory : ICURIFactory
        {
            /// <summary>
            /// Implementation of <see cref="ICURIFactory.Make(Uri, AbstractCURI?)"/> which ignores the <paramref name="absoluteOrRelativeUri"/>.
            /// </summary>
            /// <param name="absoluteOrRelativeUri"></param>
            /// <param name="anchor"></param>
            /// <returns>A <see cref="FileAbsoluteCURI"/> of the <paramref name="absoluteOrRelativeUri"/>.</returns>
            public AbstractCURI Make (Uri absoluteOrRelativeUri, AbstractCURI? anchor)
            {
                return new FileAbsoluteCURI(absoluteOrRelativeUri);
            }
        }
    }
}
