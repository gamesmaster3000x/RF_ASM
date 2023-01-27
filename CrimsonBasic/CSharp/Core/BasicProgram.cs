using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimsonBasic.CSharp.Statements;

namespace CrimsonBasic.CSharp.Core
{
    public class BasicProgram
    {
        public List<Fragment> Fragments { get; }

        public BasicProgram()
        {
            Fragments = new List<Fragment>();
        }

        public void Add(Fragment f)
        {
            Fragments.Add(f);
        }

        public void Add(BasicStatement s)
        {
            Fragment f = new Fragment(0);
            f.Add(s);
            Fragments.Add(f);
        }
    }
}
