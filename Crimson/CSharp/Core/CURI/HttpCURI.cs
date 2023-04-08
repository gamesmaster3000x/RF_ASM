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
            if (!Uri.UriSchemeHttp.Equals(uri.Scheme))
            {
                throw new UriFormatException("The");
            }
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
                return null;
            }
        }
    }
}
