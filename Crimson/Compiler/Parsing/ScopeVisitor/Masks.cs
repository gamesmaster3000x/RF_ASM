using Antlr4.Runtime.Misc;
using Compiler.AntlrBuild;
using Compiler.Common.Exceptions;
using Compiler.Parsing.Syntax;
using Compiler.Parsing.Syntax.Values;
using Compiler.Parsing.Syntax.Variables;

namespace Compiler.Parsing.ScopeVisitor
{
    internal partial class ScopeVisitor
    {
        private Mask ParseMask (CrimsonParser.MaskDeclarationContext mdCtx)
        {
            if (mdCtx is CrimsonParser.MultiMaskContext multiCtx)
            {
                FullName fullName = VisitFullName(multiCtx.name);
                Dictionary<string, ISimpleValue> parameters = VisitMultiMaskBody(multiCtx.multiBody);

                if (parameters.Count <= 0) throw new MultiMaskParametersException(fullName.ToString(), parameters);

                foreach (var p in multiCtx.multiBody._parameters)
                {
                    // return VisitMultiMaskParameter(p);
                }
            }
            else if (mdCtx is CrimsonParser.MonoMaskContext monoCtx)
            {

            }

            throw new NotImplementedException();
        }

        public override Dictionary<string, ISimpleValue> VisitMultiMaskBody ([NotNull] CrimsonParser.MultiMaskBodyContext context)
        {
            Dictionary<string, ISimpleValue> parameters = new Dictionary<string, ISimpleValue>();
            foreach (CrimsonParser.MultiMaskParameterContext p in context._parameters)
            {
                MultiMaskParameter mmp = VisitMultiMaskParameter(p);
                parameters.Add(mmp.Name, mmp.Size);
            }
            return parameters;
        }

        internal record MultiMaskParameter (string Name, ISimpleValue Size);

        public override MultiMaskParameter VisitMultiMaskParameter ([NotNull] CrimsonParser.MultiMaskParameterContext context)
        {
            string name = context.name.Text;
            ISimpleValue size = VisitDatasize(context.size);
            return new MultiMaskParameter(name, size);
        }

    }
}
