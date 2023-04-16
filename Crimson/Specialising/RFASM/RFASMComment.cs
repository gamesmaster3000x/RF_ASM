using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.Specialising.RFASM
{
    internal class RFASMComment : RFASMStatement
    {
        public string Comment { get; private set; }

        public RFASMComment(string comment) => Comment = comment;

        public override string ToString()
        {
            if (Comment.Equals("") || Comment.StartsWith("//")) return Comment;
            return "// " + Comment;
        }
    }
}
