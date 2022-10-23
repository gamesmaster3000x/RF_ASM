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
        public CrimsonParser.PackageDefinitionContext Context { get; }
        public string Name { get; }
        public Dictionary<string, Dependency> Dependencies { get; }
        public Dictionary<string, Structure> Structures { get; }
        public Dictionary<string, Function> Functions { get; }
        public Dictionary<string, GlobalVariable> Globals { get; }

        public Package(Compilation compilation, CrimsonParser.PackageDefinitionContext context)
        {
            Context = context;
            Dependencies = ResolveDependencies();
            Structures = ResolveStructures();
            Functions = ResolveFunctions();
            Globals = ResolveGlobalVariables();
        }

        private Dictionary<string, Dependency>? ResolveDependencies()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, Structure>? ResolveStructures()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, Function>? ResolveFunctions()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, GlobalVariable>? ResolveGlobalVariables()
        {
            throw new NotImplementedException();
        }
    }
}
