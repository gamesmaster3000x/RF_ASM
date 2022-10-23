using Antlr4.Runtime;
using Crimson.ANTLR.Crimson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Crimson.ANTLR.Crimson.CrimsonParser;

namespace Crimson.CSharp.Reflection
{
    internal class Package
    {
        public CompilationUnit Compilation { get; set; }
        public CrimsonParser.PackageDefinitionContext Context { get; set; }
        public string PackageName { get; set; }
        public string Path { get; set; }
        public string QualifiedName { get => Path + PackageName; }
        public Dictionary<string, Dependency> Dependencies { get; set; }
        public Dictionary<string, Structure> Structures { get; set; }
        public Dictionary<string, Function> Functions { get; set; }
        public Dictionary<string, GlobalVariable> Globals { get; set; }      
    }
}
