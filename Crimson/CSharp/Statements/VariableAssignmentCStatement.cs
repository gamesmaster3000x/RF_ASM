using Crimson.CSharp.Core;

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
    }
}