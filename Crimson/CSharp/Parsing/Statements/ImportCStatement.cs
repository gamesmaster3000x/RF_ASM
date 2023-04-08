using Crimson.CSharp.Core;
using Crimson.CSharp.Core.CURI;
using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Parsing.Tokens;
using NLog;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace Crimson.CSharp.Parsing.Statements
{
    public class ImportCStatement
    {
        public AbstractCURI URI { get; set; }
        public FullNameCToken Alias { get; set; }

        public ImportCStatement (string uri, FullNameCToken alias) : this(AbstractCURI.Create(uri), alias) { }
        public ImportCStatement (Uri uri, FullNameCToken alias) : this(AbstractCURI.Create(uri), alias) { }

        public ImportCStatement (AbstractCURI curi, FullNameCToken alias)
        {
            if (alias.HasLibrary()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{curi}' because it must only contain a member name.");
            if (!alias.HasMember()) throw new CrimsonParserException($"The alias {alias} cannot be given to the import '{curi}' because it does not contain a member name.");

            URI = curi;
            Alias = alias;
        }
    }
}