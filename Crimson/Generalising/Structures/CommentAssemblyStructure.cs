using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crimson.Generalising.Structures
{
    public class CommentAssemblyStructure : IGeneralAssemblyStructure
    {
        private string _text;

        public CommentAssemblyStructure(string text)
        {
            _text = text;
        }

        public IEnumerable<IGeneralAssemblyStructure> GetSubStructures()
        {
            return Enumerable.Empty<IGeneralAssemblyStructure>();
        }

        public override string ToString()
        {
            if (_text.Equals("") || _text.StartsWith("//")) return _text;
            return "// " + _text;
        }
    }
}
