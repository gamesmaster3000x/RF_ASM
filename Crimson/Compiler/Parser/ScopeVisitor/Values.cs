using Antlr4.Runtime.Misc;
using Compiler.AntlrBuild;
using Compiler.Common.Exceptions;
using Compiler.Parser.Syntax.Values;
using Compiler.Parsing.Syntax;
using Compiler.Parsing.Syntax.Variables;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {
        public override IComplexValue VisitComplexValue ([NotNull] CrimsonParser.ComplexValueContext context)
        {
            if (context.op != null) return VisitOperation(context.op);
            else if (context.func != null) return new FunctionCallValue(VisitFunctionCall(context.func));
            throw new CrimsonParserException("Cannot parse ComplexValueContext " + context.GetText());
        }

        public override ISimpleValue VisitSimpleValue ([NotNull] CrimsonParser.SimpleValueContext context)
        {
            if (context.id != null) return new IdentifierValue(VisitFullName(context.id));
            else if (context.raw != null) return VisitRawValue(context.raw);
            throw new CrimsonParserException("Cannot parse SimpleValueContext " + context.GetText());
        }

        public override RawValue VisitRawValue ([NotNull] CrimsonParser.RawValueContext context)
        {
            return new RawValue(context.GetText());
        }

        public override object VisitArray ([NotNull] CrimsonParser.ArrayContext context)
        {
            return base.VisitArray(context);
        }
    }
}
