using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core.Statements
{
    public class CommentBStatement : BasicStatement
    {
        private string _text;

        public CommentBStatement(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            if (_text.Equals("") || _text.StartsWith("//")) return _text;
            return "// " + _text;
        }
    }
}
