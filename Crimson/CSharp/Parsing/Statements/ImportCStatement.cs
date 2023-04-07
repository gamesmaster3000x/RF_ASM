using Crimson.CSharp.Core;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing.Tokens;
using NLog;
using static System.Formats.Asn1.AsnWriter;

namespace Crimson.CSharp.Parsing.Statements
{
    public class ImportCStatement
    {
        public Uri URI { get; set; }
        public FullNameCToken Alias { get; set; }

        public ImportCStatement (string uri, FullNameCToken alias) : this(URIs.CreateUri(uri), alias)
        {
        }

        public ImportCStatement (Uri uri, FullNameCToken alias)
        {
            if (alias.HasLibrary()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{uri}' because it must only contain a member name.");
            if (!alias.HasMember()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{uri}' because it does not contain a member name.");

            URI = uri;
            Alias = alias;
        }

        public static void VerifyUri (Uri uri)
        {
            // Check host
            if (String.IsNullOrWhiteSpace(uri.Host))
            {
                throw new UriHostException(uri);
            }
            else if (!URIs.CustomHosts.Contains(uri.Host))
            {
                throw new UriHostException(uri);
            }

            // Check path
            if (String.IsNullOrWhiteSpace(uri.AbsolutePath))
            {
                // throw new UriPathException();
            }
        }
    }
}