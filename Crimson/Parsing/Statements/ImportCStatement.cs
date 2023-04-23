using Crimson.Core;
using Crimson.Exceptions;
using Crimson.CURI;
using Crimson.Parsing.Tokens;
using NLog;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace Crimson.Parsing.Statements
{
    public class ImportCStatement
    {
        public AbstractCURI CURI { get; set; }
        public FullNameCToken Alias { get; set; }

        public ImportCStatement (string uri, FullNameCToken alias) : this(AbstractCURI.Create(uri, null), alias) { }
        public ImportCStatement (Uri uri, FullNameCToken alias) : this(AbstractCURI.Create(uri, null), alias) { }

        public ImportCStatement (AbstractCURI curi, FullNameCToken alias)
        {
            if (alias.HasLibrary()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{curi}' because it must only contain a member name.");
            if (!alias.HasMember()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{curi}' because it does not contain a member name.");

            CURI = curi;
            Alias = alias;
        }
    }
}