using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core.CURI
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
                Crimson.Panic($"{GetType().Name}: An error occurred while awaiting a read operation on {AbsolutePath}.", Crimson.PanicCode.CURI_STREAM, ex);
                throw;
            }
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri uri)
            {
                return new FileAbsoluteCURI(uri);
            }
        }
    }
}
