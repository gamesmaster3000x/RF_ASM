using Crimson.CSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Core.CURI
{
    internal class HttpCURI : AbstractCURI
    {
        public static readonly string SCHEME = "http";

        public HttpCURI (Uri uri) : base(uri)
        {
            if (!Uri.UriSchemeFile.Equals(uri.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {Uri.UriSchemeHttp}.");
        }

        public override bool Equals (AbstractCURI? other)
        {
            return other?.Uri?.Equals(Uri) ?? false;
        }

        public override async Task<Stream> GetStream ()
        {
            // New HTTP stuff
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponse = await client.GetAsync(Uri);
            return httpResponse.Content.ReadAsStream();
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri uri)
            {
                return new HttpCURI(uri);
            }
        }
    }
}
