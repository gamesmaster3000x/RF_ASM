using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using CrimsonCore.Parsing.Statements;
using CrimsonCore.Linking;
using CrimsonCore.Generalising;

namespace CrimsonCore.Parsing.Tokens.Values
{
    public class IdentifierSimpleValueCToken : SimpleValueCToken
    {
        public FullNameCToken Identifier { get; private set; }
        public ScopeVariableCStatement? ScopeVariable { get; private set; }
        public GlobalVariableCStatement? GlobalVariable { get; private set; }

        public IdentifierSimpleValueCToken (FullNameCToken identifier)
        {
            Identifier = identifier;
        }

        public override void Link (LinkingContext ctx)
        {
            // TODO Scope.FindScopeVariable(MemberName);
            ScopeVariable = ctx.CurrentScope.FindScopeVariable(Identifier.MemberName!);

            // TODO LinkingContext.GetGlobalVariable(MemberName);
            if (ScopeVariable == null)
                GlobalVariable = ctx.GetGlobalVariable(Identifier.MemberName);
        }

        public override string ToString ()
        {
            return GetText();
        }

        public override string GetText ()
        {
            return Identifier.ToString();
        }

        public override bool CanEvaluate ()
        {
            return true;
        }

        public override object Evaluate (GeneralisationContext context)
        {
            if (context.Globals.TryGetValue(Identifier.ToString(), out GlobalVariableCStatement global))
            {

            }
            throw new NullReferenceException($"Error generalising '{GetType()}' '{this}': there is no variable {Identifier}");
        }
    }
}