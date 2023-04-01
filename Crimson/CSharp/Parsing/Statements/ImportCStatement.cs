using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing.Tokens;

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

            URI = uri;
            Alias = alias;
        }
    }
}