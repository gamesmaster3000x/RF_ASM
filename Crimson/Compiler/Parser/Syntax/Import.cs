using Compiler.Common.CURI;
using Compiler.Common.Exceptions;

namespace Compiler.Parser.Syntax
{
    public class Import
    {
        public AbstractCURI CURI { get; set; }
        public FullName Alias { get; set; }

        public Import (string uri, FullName alias) : this(AbstractCURI.Create(uri, null), alias) { }
        public Import (Uri uri, FullName alias) : this(AbstractCURI.Create(uri, null), alias) { }

        public Import (AbstractCURI curi, FullName alias)
        {
            if (alias.HasLibrary()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{curi}' because it must only contain a member name.");
            if (!alias.HasMember()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{curi}' because it does not contain a member name.");

            CURI = curi;
            Alias = alias;
        }
    }
}