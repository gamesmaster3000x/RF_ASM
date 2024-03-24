using Antlr4.Runtime.Misc;
using Compiler.AntlrBuild;
using Compiler.Parser.Syntax;
using Compiler.Parser.Syntax.Functions;
using Compiler.Parser.Syntax.Values;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {

        public override Function.Header VisitFunctionHeader ([NotNull] CrimsonParser.FunctionHeaderContext context)
        {
            FullName identifier = VisitFullName(context.name);
            List<Function.Parameter> parameters = VisitParameterList(context.parameters);
            return new Function.Header(identifier, parameters);
        }

        public override List<Function.Parameter> VisitParameterList ([NotNull] CrimsonParser.ParameterListContext context)
        {
            List<Function.Parameter> parameters = new List<Function.Parameter>();
            foreach (CrimsonParser.ParameterContext paCtx in context.parameter())
            {
                FullName identifier = new FullName(paCtx.name.Text);
                ISimpleValue size = VisitDatasize(paCtx.size);
                Function.Parameter parameter = new Function.Parameter(size, identifier);
                parameters.Add(parameter);
            }
            return parameters;
        }
        public override FunctionCall VisitFunctionCall ([NotNull] CrimsonParser.FunctionCallContext context)
        {
            FullName identifier = VisitFullName(context.name);
            IList<ISimpleValue> arguments = VisitArguments(context.arguments());
            FunctionCall call = new FunctionCall(identifier, arguments);
            return call;
        }

        public override IList<ISimpleValue> VisitArguments ([NotNull] CrimsonParser.ArgumentsContext context)
        {
            IList<ISimpleValue> arguments = new List<ISimpleValue>();
            foreach (CrimsonParser.SimpleValueContext rvlCxt in context.simpleValue())
            {
                ISimpleValue value = VisitSimpleValue(rvlCxt);
                arguments.Add(value);
            }
            return arguments;
        }
    }
}
