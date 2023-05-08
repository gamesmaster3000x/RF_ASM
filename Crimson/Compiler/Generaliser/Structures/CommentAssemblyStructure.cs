using Compiler.Generaliser;

namespace Compiler.Generaliser.Structures
{
    public class CommentAssemblyStructure : IGeneralAssemblyStructure
    {
        private string _text;

        public CommentAssemblyStructure (string text)
        {
            _text = text;
        }

        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures ()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }

        public override string ToString ()
        {
            if (_text.Equals("") || _text.StartsWith("//")) return _text;
            return "// " + _text;
        }
    }
}
