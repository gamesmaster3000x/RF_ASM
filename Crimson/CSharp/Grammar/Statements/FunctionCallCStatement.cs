using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    public class FunctionCallCStatement : ICrimsonStatement
    {
        private FullNameCToken identifier;
        private FunctionCStatement? targetFunction;
        private IList<SimpleValueCToken> arguments;
        private bool _linked = false;

        public static readonly string FUNCTION_RETURN_VARIABLE_NAME = "FUNC_RETURN";

        public FunctionCallCStatement(FullNameCToken identifier, IList<SimpleValueCToken> arguments) : base()
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
        public Fragment GetCrimsonBasic()
        {
            Fragment f = new Fragment(0);

            // Add arguments
            List<string> argumentHolders = new List<string>();
            foreach (var argValue in arguments)
            {
                if (argValue is IdentifierSimpleValueCToken irvct)
                {
                    argumentHolders.Add(irvct.Identifier.ToString());
                }
                else if (argValue is RawResolvableValueCToken rrvct)
                {
                    argumentHolders.Add(rrvct.Content);
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
            f.Add(new JumpBStatement(targetFunction!.Name.ToString()));

            // Store result
            string returnName = FlattenerHelper.GetUniqueResolvableValueFieldName();
            f.Add(new RegSetBStatement("REG_RETURN", returnName));
            f.Add(new SetBStatement(returnName, -1, FUNCTION_RETURN_VARIABLE_NAME));

            f.ResultHolder = returnName;

            return f;
        }

        public void Link(LinkingContext ctx)
        {
            if (IsLinked()) return;

            targetFunction = LinkerHelper.LinkFunctionCall(identifier, ctx);

            foreach (var a in arguments)
            {
                a.Link(ctx);
            }

            _linked = true;
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }
    }
}