using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Statements
{
    public class ImportCStatement
    {
        public string Path { get; set; }
        public FullNameCToken Alias { get; set; }

        public ImportCStatement (string path, FullNameCToken alias)
        {
            if (alias.HasLibrary()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{path}' because it must only contain a member name.");
            if (!alias.HasMember()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{path}' because it does not contain a member name.");

            Path = path;
            Alias = alias;
        }
    }
}