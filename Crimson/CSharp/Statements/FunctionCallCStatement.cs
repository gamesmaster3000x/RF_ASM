using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    public class FunctionCallCStatement : InternalStatement
    {
        private string identifier;
        private FunctionCStatement? targetFunction;
        private IList<ResolvableValueCToken> arguments;

        public static readonly string FUNCTION_RETURN_VARIABLE_NAME = "FUNC_RETURN";

        public FunctionCallCStatement(string identifier, IList<ResolvableValueCToken> arguments): base()
        {
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
        public override Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);

            // Add arguments
            List<string> argumentHolders = new List<string>();
            foreach (var argValue in arguments)
            {

                // If argument is FUNCTION_CALL
                if (argValue.TypeOfValue == ResolvableValueCToken.ValueType.FUNCTION_CALL)
                {
                    f.Add(new JumpBStatement(argValue.FunctionContent!.identifier));
                    //f.Add(new CommentBStatement("^^ FCCS Why is this not linked!? (utils.otherthing should be linked)"));
                    string argReturnName = FlattenerHelper.GetUniqueResolvableValueFieldName();
                    f.Add(new SetBStatement(argReturnName, FUNCTION_RETURN_VARIABLE_NAME));
                    //f.Add(new CommentBStatement("^^ FCCS Perhaps need to dereference and copy/change ownership to not be overwritten?"));

                    argumentHolders.Add(argReturnName);
                }

                // If argument is BOOLEAN, NUMBER, NULL, IDENTIFIER
                else if (
                    argValue.TypeOfValue == ResolvableValueCToken.ValueType.BOOLEAN
                    || argValue.TypeOfValue == ResolvableValueCToken.ValueType.NUMBER
                    || argValue.TypeOfValue == ResolvableValueCToken.ValueType.NULL
                    || argValue.TypeOfValue == ResolvableValueCToken.ValueType.IDENTIFIER
                    )
                {
                    argumentHolders.Add(argValue.StringContent!);
                } 

                else
                {
                    throw new FlatteningException($"Illegal type {argValue.GetType()} for ResolvableValue " + argValue);
                }
            }

            for (int i = 0; i < argumentHolders.Count; i++)
            {
                f.Add(new CommentBStatement($"arg{i}={argumentHolders[i]}"));
            }

            // Jump
            f.Add(new JumpBStatement(identifier));

            // Store result
            string returnName = FlattenerHelper.GetUniqueResolvableValueFieldName();
            f.Add(new HeapBStatement(HeapBStatement.HeapOperation.ALLOCATE, returnName, "6969"));
            f.Add(new RegisterBStatement(RegisterBStatement.RegisterOperation.SET, "REG_RETURN", returnName));
            f.Add(new SetBStatement(returnName, FUNCTION_RETURN_VARIABLE_NAME));
            
            f.ResultHolder = returnName;

            return f;
        }

        public override void Link(LinkingContext ctx)
        {
            if (IsLinked()) return;

            targetFunction = LinkerHelper.LinkFunctionCall(identifier, ctx);

            foreach (var a in arguments)
            {
                a.Link(ctx);
            }

            SetLinked(true);
        }
    }
}