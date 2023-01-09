using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class VariableAssignmentCStatement: InternalStatement
    {
        public VariableAssignmentCStatement(string identifier, ResolvableValueCToken value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; set; }
        public ResolvableValueCToken Value { get; }

        public override void Link(LinkingContext ctx)
        {
            Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            Value.Link(ctx);
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment result = new Fragment(0);

            if (Value == null) throw new FlatteningException("Illegal value assignment to " + Identifier + " (Proposed value is compiler-null)");
            result.Add(Value.GetCrimsonBasic());
            result.Add(new VariableAssignBStatement(Identifier, "VAR_ASSIGN_C_VAL"));

            return result;
        }
    }
}