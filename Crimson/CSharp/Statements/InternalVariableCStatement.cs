using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public class InternalVariableCStatement : InternalStatement
    {
        private CrimsonTypeCToken type;
        public string identifier { get; set; }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier)
        {
            this.type = type;
            this.identifier = identifier;
        }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier, ResolvableValueCToken? value) : this(type, identifier)
        {
            Value = value;
        }

        public ResolvableValueCToken? Value { get; }

        public override void Link(LinkingContext ctx)
        {
            return;
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment statements = new Fragment(0);

            if (Value != null)
            {
                Fragment valueStatements = Value.GetCrimsonBasic();
                statements.Add(valueStatements);
            }
            statements.Add(new VariableDeclareBStatement(identifier));
            statements.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, identifier, type.GetByteSize().ToString()));
            statements.Add(new SetBStatement(identifier, "INT_VAR_ASSIGN_VAL"));

            return statements;
        }
    }
}