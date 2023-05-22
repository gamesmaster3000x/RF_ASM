namespace Compiler.Specialiser
{
    public class Fragment
    {
        private List<AbstractSpecificAssemblyStatement> _statements;
        private List<int> _indents;
        private int _indentation;
        public string? ResultHolder { get; set; }

        public Fragment (int indentation)
        {
            _statements = new List<AbstractSpecificAssemblyStatement>();
            _indents = new List<int>();
            _indentation = indentation;
        }


        public void Add (AbstractSpecificAssemblyStatement statement)
        {
            if (statement == null) throw new ArgumentNullException("Cannot add a null BasicStatement to a Fragment");
            _statements.Add(statement);
            _indents.Add(_indentation);
        }

        public void Add (IList<AbstractSpecificAssemblyStatement> statements)
        {
            foreach (var s in statements)
                Add(s);
        }

        public void Add (Fragment fragment)
        {
            for (int i = 0; i < fragment._statements.Count; i++)
            {
                _statements.Add(fragment._statements[i]);
                _indents.Add(_indentation + fragment._indentation + fragment._indents[i]);
            }
        }

        public List<string> GetLines ()
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < _statements.Count; i++)
            {
                int j = _indents[i];
                var s = _statements[i];
                string indent = string.Concat(Enumerable.Repeat("  ", j));
                lines.Add(indent + s);
            }
            return lines;
        }

        public Fragment WithIndentation (int indentation)
        {
            _indentation = indentation;
            return this;
        }

    }
}
