using Compiler.Mapping;
using Compiler.Generalising;
using Compiler.Generalising.Structures;
using Compiler.Common.Exceptions;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.Syntax.Functions
{
    public class FunctionCall : IAssemblable, IMappable
    {
        public FullName Identifier { get; private set; }
        private Function? targetFunction;
        private IList<ISimpleValue> arguments;

        public static readonly string FUNCTION_RETURN_VARIABLE_NAME = "FUNC_RETURN";

        public FunctionCall (FullName identifier, IList<ISimpleValue> arguments) : base()
        {
            if (!identifier.HasMember()) throw new CrimsonParserException($"Name {identifier} must contain a member name.");

            Identifier = identifier;
            this.arguments = arguments;
        }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
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

        public void Map (MappingContext ctx)
        {
            targetFunction = MapperHelper.GetLinkedFunctionForCall(Identifier, ctx);

            foreach (var a in arguments)
                (a as IMappable)?.Map(ctx);
        }
    }
}