using Compiler.Generaliser;

namespace Compiler.Parser.Syntax.Values
{
    public interface ISimpleValue : IMappable
    {
        public abstract bool CanEvaluateDuringCompile ();
        public abstract object Evaluate (GeneralisationContext context);
    }
}