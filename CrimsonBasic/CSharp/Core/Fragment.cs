using CrimsonBasic.CSharp.Core.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimsonBasic.CSharp.Core
{
    public class Fragment
    {
        private List<BasicStatement> _statements;
        private int _indentation;

        public Fragment(int indentation)
        {
            _statements = new List<BasicStatement>();
            _indentation = indentation;
        }

        public void Add(BasicStatement statement)
        {
            if (statement == null) throw new ArgumentNullException("Cannot add a null BasicStatement to a Fragment");
            _statements.Add(statement);
        }

        public void Add(IList<BasicStatement> statements)
        {
            foreach (var s in statements)
            {
                Add(s);
            }
        }

        public void Add(Fragment fragment)
        {
            Add(fragment._statements);
        }

        public Fragment WithIndentation(int indentation)
        {
            _indentation = indentation;
            return this;
        }
    }
}
