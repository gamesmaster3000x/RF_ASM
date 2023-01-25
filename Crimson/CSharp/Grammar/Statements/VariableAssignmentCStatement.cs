using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class VariableAssignmentCStatement : InternalStatement
    {
        public VariableAssignmentCStatement(string identifier, ComplexValueCToken value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; set; }
        public ComplexValueCToken Value { get; }

        public override void Link(LinkingContext ctx)
        {
            Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            Value.Link(ctx);
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment result = new Fragment(0);

            if (Value == null) throw new FlatteningException("Illegal value assignment to " + Identifier + " (Proposed value is compiler-null)");
            result.Add(Value.GetBasicFragment());
            result.Add(new SetBStatement(Identifier, "VAR_ASSIGN_C_VAL"));

            return result;
        }
    }
}