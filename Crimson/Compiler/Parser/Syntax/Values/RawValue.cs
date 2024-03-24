using Compiler.Generaliser;
using Compiler.Mapper;

namespace Compiler.Parser.Syntax.Values
{
    public class RawValue : ISimpleValue
    {
        public string Content { get; }

        public RawValue (string s)
        {
            Content = s;
        }

        public override string ToString ()
        {
            return Content;
        }


        public object Evaluate (GeneralisationContext context)
        {
            return Content;
        }

        public bool CanEvaluateDuringCompile ()
        {
            return true;
        }

        public void Map (MappingContext context)
        {
        }
    }
}