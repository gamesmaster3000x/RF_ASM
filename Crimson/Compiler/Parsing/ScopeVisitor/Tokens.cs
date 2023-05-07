using Antlr4.Runtime.Misc;
using Compiler.AntlrBuild;
using Compiler.Parsing.Syntax;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {
        public override ISimpleValue VisitDatasize ([NotNull] CrimsonParser.DatasizeContext context)
        {
            return VisitSimpleValue(context.sizeVal);
        }

        public override FullName VisitFullName ([NotNull] CrimsonParser.FullNameContext context)
        {
            return new FullName(context.libraryName != null ? context.libraryName.Text : "", context.memberName.Text);
        }
    }
}
