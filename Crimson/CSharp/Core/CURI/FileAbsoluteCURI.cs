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
            if (!Uri.UriSchemeFile.Equals(uri.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {Uri.UriSchemeFile}.");
        }

        public override bool Equals (AbstractCURI? other)
        {
            return other?.Uri?.Equals(Uri) ?? false;
        }

        public override async Task<Stream> GetStream ()
        {
            return await Task.Run(() => File.OpenRead(AbsolutePath));
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
