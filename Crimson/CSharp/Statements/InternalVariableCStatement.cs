using Crimson.CSharp.Core;
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

        public override IList<BasicStatement> GetCrimsonBasic()
        {
            List<BasicStatement> statements = new List<BasicStatement>();

            if (Value != null)
            {
                IList<BasicStatement> valueStatements = Value.GetCrimsonBasic();
                statements.AddRange(valueStatements);
            }
            statements.Add(new VariableDeclareBStatement(identifier));
            statements.Add(new MemoryAllocateBStatement(identifier, type.GetByteSize()));
            statements.Add(new VariableAssignBStatement(identifier, "INT_VAR_ASSIGN_VAL"));

            return statements;
        }
    }
}