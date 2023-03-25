using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Generalising;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Tokens;
using Crimson.CSharp.Parsing.Tokens.Values;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Statements
{
    public class ScopeVariableCStatement : AbstractCrimsonStatement
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

        public override void Link (LinkingContext ctx)
        {
            Identifier = ctx.GetUniqueScopeVariableName(Identifier);
            Size.Link(ctx);
            Identifier.Link(ctx);
            Linked = true;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            // TODO Scope variable C statement GeneralisationContext.AllocStack()
            return new ArbitraryAssemblyStructure(ToString()!);
        }
    }
}