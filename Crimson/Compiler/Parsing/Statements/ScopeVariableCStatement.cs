using Compiler.Generalising.Structures;

using Compiler.Mapping;
using Compiler.Parsing.Tokens.Values;
using Compiler.Parsing.Tokens;
using Compiler.Generalising;
using Compiler.Common.Exceptions;

namespace Compiler.Parsing.Statements
{
    public class ScopeVariableCStatement : AbstractCrimsonStatement, INamed
    {
        public SimpleValueCToken Size { get; set; }
        public FullNameCToken Identifier { get; private set; }

        public ScopeVariableCStatement (FullNameCToken identifier, SimpleValueCToken size)
        {
            Size = size;
            Identifier = identifier;

            if (identifier == null) throw new CrimsonParserException("Null identifier");
            if (identifier.HasLibrary()) throw new CrimsonParserException($"Identifier {identifier} for internal variable may not contain a library name.");
            if (!identifier.HasMember()) throw new CrimsonParserException($"Identifier {identifier} for internal variable must have a member name.");
        }

        public override void Link (MappingContext ctx)
        {
            Identifier = ctx.GetUniqueScopeVariableName(Identifier);
            Size.Link(ctx);
            Identifier.Link(ctx);
            Mapped = true;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            // TODO Scope variable C statement GeneralisationContext.AllocStack()
            return new ArbitraryAssemblyStructure(ToString()!);
        }

        public FullNameCToken GetName ()
        {
            return Identifier;
        }

        public void SetName (FullNameCToken name)
        {
            Identifier = name;
        }
    }
}