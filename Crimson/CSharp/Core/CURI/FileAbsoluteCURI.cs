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

        public FileAbsoluteCURI (Uri uri) : base(uri)
        {
            if (!Uri.UriSchemeHttp.Equals(uri.Scheme))
            {
                throw new UriFormatException("The");
            }
        }

        public override bool Equals (AbstractCURI? other)
        {
            throw new NotImplementedException();
        }

        public override async Task<Stream> GetStream ()
        {
            string trimmed = uri.AbsolutePath.TrimStart('/', '\\', '\n', '\t');
            string uriPath = WebUtility.UrlDecode(trimmed);

            // file://abs.crimson/C:/etc/thing.crm
            // C://etc/thing.crm
            if (ABSOLUTE_SCHEME.Equals(uri.Host))
            {
                return uriPath;
            }

            return File.OpenRead(path); // TODO IO error here (path not found)#
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri uri)
            {

            }
        }
    }
}
