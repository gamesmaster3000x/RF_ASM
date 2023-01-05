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
        public CommentBStatement(string text): base(text)
        {
            Text = text;
        }

        public override string ToString()
        {
            if (Text.Equals("") || Text.StartsWith("//")) return Text;
            return "// " + Text;
        }
    }
}
