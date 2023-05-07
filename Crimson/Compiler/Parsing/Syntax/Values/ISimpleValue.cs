using Compiler.Generalising;

namespace Compiler.Parsing.Syntax.Values
{
    public interface ISimpleValue : IMappable
    {
        public abstract bool CanEvaluateDuringCompile ();
        public abstract object Evaluate (GeneralisationContext context);
    }
}