using Crimson.CSharp.Reflection;

namespace Crimson.CSharp.Statements
{
    internal class ElseBlock
    {
        public ElseBlock(IList<InternalStatement> statements)
        {
            Statements = statements;
        }

        public IList<InternalStatement> Statements { get; }
    }
}