using Crimson.CSharp.Core;

namespace Crimson.CSharp.Statements
{
    internal class VariableAssignment: InternalStatement
    {
        public VariableAssignment()
        {
        }

        public VariableAssignment(string identifier, ResolvableValue value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; set; }
        public ResolvableValue Value { get; }

        public override void Link(LinkingContext ctx)
        {
            Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            Value.Link(ctx);
        }
    }
}