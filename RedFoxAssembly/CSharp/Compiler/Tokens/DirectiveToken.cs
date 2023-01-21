using RedFoxAssembly.CSharp.TokenParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Compiler.Tokens
{
    internal class DirectiveToken : AbstractToken
    {
        private RFASMCompilerMetadata meta;
        private Directives directive;
        public DirectiveToken(Directives value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            TokenType = TokenType.DIRECTIVE;
            RawValue = Enum.GetName(value.GetType(), value);
            directive = value;
        }

        public override byte[] GetBytes()
        {
            return new byte[0];
        }

        // TODO DirectiveToken syntax
        public override bool CheckFollowing(IToken[] following)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compiler"></param>
        /// <param name="following"></param>
        /// <returns>The number of tokens after this one to skip/ignore because they've been resolved as part of this one.</returns>
        /// <exception cref="CompilationException"></exception>
        public int ResolveDirective(RFASMCompiler compiler, List<IToken> following)
        {
            switch (directive)
            {
                case Directives.WIDTH:
                    return ResolveWidth(compiler, following);
                case Directives.VAL:
                    return ResolveVal(compiler, following);
            }

            throw new CompilationException("Cannot resolve directive " + directive.ToString());
        }

        private int ResolveWidth(RFASMCompiler compiler, List<IToken> following)
        {
            List<IToken> result = new List<IToken>();

            IToken token = following[0];
            if (token is not RawValueToken)
            {
                throw new PreCompilationException("Failed to resolve width directive. Cannot set data width to non-RawValueToken " + token);
            }

            RawValueToken rawValueToken = (RawValueToken)token;
            int width = short.Parse(rawValueToken.RawValue);

            meta.DataWidth = width;
            Console.WriteLine("Directive: Data width " + width);

            // Skip the next token
            return 1;
        }

        private int ResolveVal(RFASMCompiler compiler, List<IToken> following)
        {
            string key = following[0].GetRawValue();

            if (following[1] is not AbstractValueToken)
            {
                throw new NotImplementedException("ResolveVal Directive Token");
            }

            AbstractValueToken valueToken = (AbstractValueToken)following[1];
            TokenTemplate template = new TokenTemplate(valueToken.ValueType, valueToken.RawValue, meta);

            meta.constants.Add(key, template);
            Console.WriteLine("Directive: Set value " + key + "=" + template.ToString());

            // Skip the next two tokens
            return 2;
        }
    }
}
