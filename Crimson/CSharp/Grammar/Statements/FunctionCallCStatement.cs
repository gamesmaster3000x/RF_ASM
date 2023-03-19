using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    public class FunctionCallCStatement : AbstractCrimsonStatement
    {
        private FullNameCToken identifier;
        private FunctionCStatement? targetFunction;
        private IList<SimpleValueCToken> arguments;

        public static readonly string FUNCTION_RETURN_VARIABLE_NAME = "FUNC_RETURN";

        public FunctionCallCStatement (FullNameCToken identifier, IList<SimpleValueCToken> arguments) : base()
        {
            if (!identifier.HasMember()) throw new CrimsonParserException($"Name {identifier} must contain a member name.");

            this.identifier = identifier;
            this.arguments = arguments;
        }

        /// <summary>
        /// 
        /// help( get(1), 5 )
        /// 
        /// var fcal_0 = get (1);
        /// help (fcal_0, 5)
        /// 
        /// </summary>
        /// <returns></returns>
        public override Fragment GetCrimsonBasic ()
        {
            Fragment f = new Fragment(0);
            f.Add(new CommentBStatement("FC start"));

            // Allocate space for input/output
            int inputSize = CalculateInputBufferSize();
            int outputSize = CalculateOutputBufferSize();
            int total = inputSize + outputSize;
            f.Add(new IncSpBStatement(total));

            f.Add(new CommentBStatement("FC end"));
            return f;
        }

        private int CalculateInputBufferSize ()
        {
            int inputSize = 0;
            FunctionCStatement.Header header = targetFunction!.FunctionHeader;
            foreach (var param in header.Parameters)
            {
                inputSize += param.Type.GetSize();
            }
            return inputSize;
        }

        private int CalculateOutputBufferSize ()
        {
            FunctionCStatement.Header header = targetFunction!.FunctionHeader;
            int outputSize = header.ReturnType.GetSize();
            return outputSize;
        }

        public override void Link (LinkingContext ctx)
        {
            if (Linked) return;

            targetFunction = LinkerHelper.LinkFunctionCall(identifier, ctx);

            foreach (var a in arguments)
            {
                a.Link(ctx);
            }

            Linked = true;
        }
    }
}