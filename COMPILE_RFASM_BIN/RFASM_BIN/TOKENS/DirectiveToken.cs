
using RFASM_COMPILER.TOKEN_PARSER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFASM_COMPILER.RFASM_BIN.TOKENS
{
    internal class DirectiveToken: AbstractToken
    {
        private RFASMCompilerMetadata meta;
        private Directives directive;
        public DirectiveToken(Directives value, RFASMCompilerMetadata meta)
        {
            this.meta = meta;
            Type = TokenType.DIRECTIVE;
            RawValue = Enum.GetName(value.GetType(), value);
            directive = value;
        }

        public override byte[] GetBytes()
        {
            return new byte[0];
        }

        // TODO DirectiveToken syntax
        public override bool HasCorrectSyntax(IToken[] following)
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
            switch (directive) {
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
            int width = Int16.Parse(rawValueToken.RawValue);

            meta.DataWidth = width;
            Console.WriteLine("Directive: Data width " + width);

            // Skip the next token
            return 1;
        }

        private int ResolveVal(RFASMCompiler compiler, List<IToken> following)
        {
            string key = following[0].GetRawValue();
            byte[] value = following[1].GetBytes();
            meta.constants.Add(key, value);
            Console.WriteLine("Directive: Set value " + key + "=" + BitConverter.ToString(value));

            // Skip the next two tokens
            return 2;
        }
    }
}
