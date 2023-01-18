using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public class InternalVariableCStatement : InternalStatement
    {
        private CrimsonTypeCToken type;
        public string identifier { get; set; }
        public ResolvableValueCToken Value { get; }
        public bool IsAllocation { get; }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier, ResolvableValueCToken value, bool isAllocation)
        {
            this.type = type;
            this.identifier = identifier;
            this.Value = value;
            this.IsAllocation = isAllocation;

            if (identifier == null) throw new ParserException("Null identifier");
            if (type == null) throw new ParserException($"Null type for variable {identifier}");
            if (Value == null) throw new ParserException($"Cannot use null value for variable {identifier} (must assign value or allocate memory)");
        }

        public override void Link(LinkingContext ctx)
        {
            Value.Link(ctx);
            return;
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment statements = new Fragment(0);

            statements.Add(new VariableDeclareBStatement(identifier));

            // int i = allocate(6);
            if (IsAllocation)
            {
                Fragment sizeValue = Value.GetCrimsonBasic();
                statements.Add(sizeValue);
                statements.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, identifier, sizeValue.ResultHolder!));
                // result_holder = result
                // stack allocate i result_holder;
            }
            // int i = (6 + 5);
            else
            {
                Fragment valueStatements = Value.GetCrimsonBasic();
                statements.Add(valueStatements);
                statements.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, identifier, type.GetByteSize().ToString()));
                statements.Add(new SetBStatement(identifier, "INT_VAR_ASSIGN_VAL"));

            }
            
            return statements;
        }
    }
}