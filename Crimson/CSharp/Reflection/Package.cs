using Antlr4.Runtime;
using Crimson.ANTLR.Crimson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Reflection
{
    internal class Package
    {
        public string name;
        public Dictionary<string, Dependency> dependencies = new Dictionary<string, Dependency>();
        public Dictionary<string, Structure> structures = new Dictionary<string, Structure>();
        public Dictionary<string, Function> functions = new Dictionary<string, Function>();
        public Dictionary<string, GlobalVariable> globals = new Dictionary<string, GlobalVariable>();

        public Package(Compilation compilation, CrimsonParser.PackageDefinitionContext context)
        {
            Context = context;
        }

        public CrimsonParser.PackageDefinitionContext Context { get; }
    }
}
