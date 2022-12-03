
using Crimson.AntlrBuild;

namespace Crimson.CSharp.Reflection
{
    internal class PackageDependency : Dependency
    {
        public CrimsonParser.PackageDependencyContext Dep { get; }
        public CompilationUnit Compilation { get; }
        public string PackageName { get; set; }
        public string Path { get; set; }
        public string CustomName { get; set; }

        public PackageDependency(CrimsonParser.PackageDependencyContext dep, CompilationUnit compilation)
        {
            Dep = dep;
            Compilation = compilation;
            PackageName = dep.packageName.Text;
            Path = dep.path.Text;
            CustomName = dep.customName.Text;
        }
    }
}