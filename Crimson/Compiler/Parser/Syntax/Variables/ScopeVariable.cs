using Compiler.Common.Exceptions;
using Compiler.Parser.Syntax.Values;
using Compiler.Mapper;
using Compiler.Parser.Syntax;

namespace Compiler.Parser.Syntax.Variables
{
    public class ScopeVariable : IMappable, INamed
    {
        public ISimpleValue Size { get; set; }
        public FullName Identifier { get; private set; }

        public ScopeVariable (FullName identifier, ISimpleValue size)
        {
            Size = size;
            Identifier = identifier;

            if (identifier == null) throw new CrimsonParserException("Null identifier");
            if (identifier.HasLibrary()) throw new CrimsonParserException($"Identifier {identifier} for internal variable may not contain a library name.");
            if (!identifier.HasMember()) throw new CrimsonParserException($"Identifier {identifier} for internal variable must have a member name.");
        }

        public void Map (MappingContext ctx)
        {
            Identifier = ctx.GetUniqueScopeVariableName(Identifier);
            Size.Map(ctx);
            Identifier.Map(ctx);
        }

        public FullName GetName ()
        {
            return Identifier;
        }

        public void SetName (FullName name)
        {
            Identifier = name;
        }
    }
}