using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing.Tokens;
using NLog;

namespace Crimson.CSharp.Parsing.Statements
{
    public class ImportCStatement
    {
        public Uri URI { get; set; }
        public FullNameCToken Alias { get; set; }

        public ImportCStatement (Uri uri, FullNameCToken alias)
        {
            if (alias.HasLibrary()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{uri}' because it must only contain a member name.");
            if (!alias.HasMember()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{uri}' because it does not contain a member name.");

            // TODO Import must vet own URIs
            string originalUriText = importCtx.uri.Text;
            string trimmedUriText = originalUriText.Trim(' ', '\t', '\n', '\v', '\f', '\r', '"');
            if (!originalUriText.Equals(trimmedUriText, StringComparison.OrdinalIgnoreCase))
            {
                LOGGER.Debug($"Trimmed URI {originalUriText} to {trimmedUriText}");
            }
            if (String.IsNullOrWhiteSpace(uri.Host))
            {
                throw new NullReferenceException($"The URI {uri} has no host!");
            }

            URI = uri;
            Alias = alias;
        }
    }
}