using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Generalising;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Tokens;

namespace Crimson.CSharp.Parsing.Statements
{
    public class FunctionCallCStatement : AbstractCrimsonStatement
    {
        public FullNameCToken Identifier { get; private set; }
        private FunctionCStatement? targetFunction;
        private IList<SimpleValueCToken> arguments;

        public static readonly string FUNCTION_RETURN_VARIABLE_NAME = "FUNC_RETURN";

        public FunctionCallCStatement (FullNameCToken identifier, IList<SimpleValueCToken> arguments) : base()
        {
            if (!identifier.HasMember()) throw new CrimsonParserException($"Name {identifier} must contain a member name.");

            this.Identifier = identifier;
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
        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ScopeAssemblyStructure scope = new ScopeAssemblyStructure();
            scope.AddSubStructure(new CommentAssemblyStructure("FC start"));

            // Allocate space for input/output
            //int inputSize = CalculateInputBufferSize();
            //int outputSize = CalculateOutputBufferSize();

            // Push inputs onto stack

            // Don't need to store return address
            // because JSR already does that on the
            // CPU stack.

            // Increment stack pointer
            //int total = inputSize + outputSize;
            //f.Add(new IncSpBStatement(total));

            // Jump to subroutine
            scope.AddSubStructure(new JumpAssemblyStructure(targetFunction!.Name.ToString()));

            scope.AddSubStructure(new CommentAssemblyStructure("FC end"));
            return scope;
        }

        public override void Link (LinkingContext ctx)
        {
            if (Linked) return;

            targetFunction = LinkerHelper.LinkFunctionCall(Identifier, ctx);

            foreach (var a in arguments)
                a.Link(ctx);

            Linked = true;
        }
    }
}