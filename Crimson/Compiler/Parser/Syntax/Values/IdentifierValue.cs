using Compiler.Parser.Syntax.Variables;
using Compiler.Mapper;
using Compiler.Generaliser;

namespace Compiler.Parser.Syntax.Values
{
    public class IdentifierValue : ISimpleValue, IMappable
    {
        public FullName Identifier { get; private set; }
        public ScopeVariable? ScopeVariable { get; private set; }
        public GlobalVariable? GlobalVariable { get; private set; }

        public IdentifierValue (FullName identifier)
        {
            Identifier = identifier;
        }

        public void Map (MappingContext ctx)
        {
            // TODO Scope.FindScopeVariable(MemberName);
            ScopeVariable = ctx.CurrentScope.FindScopeVariable(Identifier.MemberName!);

            // TODO LinkingContext.GetGlobalVariable(MemberName);
            if (ScopeVariable == null)
                GlobalVariable = ctx.GetGlobalVariable(Identifier.MemberName);
        }

        public bool CanEvaluateDuringCompile ()
        {
            return true;
        }

        public object Evaluate (GeneralisationContext context)
        {
            if (context.Globals.TryGetValue(Identifier.ToString(), out GlobalVariable global))
            {

            }
            throw new NullReferenceException($"Error generalising '{GetType()}' '{this}': there is no variable {Identifier}");
        }
    }
}