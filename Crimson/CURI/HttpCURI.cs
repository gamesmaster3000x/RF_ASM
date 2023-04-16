using Crimson.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CURI
{
    internal class HttpCURI : AbstractCURI
    {
        public static readonly string SCHEME = "http";

        public HttpCURI (Uri uri) : base(uri)
        {
            if (!Uri.UriSchemeHttp.Equals(uri.Scheme)) throw new UriFormatException($"{GetType()} may only take URIs of scheme {Uri.UriSchemeHttp}. Found '{uri.Scheme}'.");
        }

        public override bool Equals (AbstractCURI? other)
        {
            return other?.Uri?.Equals(Uri) ?? false;
        }

        public override Stream GetStream ()
        {
            try
            {
                // New HTTP stuff
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> responseTask = client.GetAsync(Uri);
                responseTask.Wait();
                return responseTask.Result.Content.ReadAsStream();
            }
            catch (Exception ex)
            {
                Crimson.Panic($"{GetType().Name}: An error occurred while fetching {Uri}.", Crimson.PanicCode.CURI_STREAM, ex);
                throw;
            }
        }

        public class Factory : ICURIFactory
        {
            public AbstractCURI Make (Uri relativeOrAbsoluteUri, AbstractCURI? anchor)
            {
                return new HttpCURI(uri);
            }
        }
    }
}
