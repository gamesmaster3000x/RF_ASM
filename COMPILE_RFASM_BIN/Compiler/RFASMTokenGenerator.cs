using Microsoft.Win32;
using RedFoxAssembly.Compiler.Tokens;
using RedFoxAssembly.TokenParser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace RedFoxAssembly.Compiler
{
    internal class RFASMTokenGenerator : ITokenGenerator
    {


        // Good Token: [A-Za-z0-9_]+((?=[ \n])|$) Match a string of alphanumeric characters (or underscore) followed by a space, newline or end of line
        // Ignore: /.+ Anything after a slash
        // Bad Token: [^A-Za-z0-9_] Contains anything which isn't alphanumeric (or underscore)
        public static readonly Regex GOOD_TOKEN = new Regex(@"[A-Za-z0-9_.:&*#?]+((?=[ \n])|$)");
        public static readonly Regex IGNORE_TOKEN = new Regex(@"/.+");
        public static readonly Regex BAD_TOKEN = new Regex(@"[^A-Za-z0-9_ .:&*#?]");


        private ITokenParserMetadata meta;

        public RFASMTokenGenerator(ITokenParserMetadata meta)
        {
            this.meta = meta;
        }

        public IToken GetToken(string value, ITokenParserMetadata meta)
        {
            RFASMCompilerMetadata metadata = (RFASMCompilerMetadata)meta;
            // Raw value
            if (value.StartsWith("#", StringComparison.Ordinal))
            {
                return new RawValueToken(value, metadata);
            }
            // Register address
            else if (value.StartsWith("*", StringComparison.Ordinal))
            {
                return new RegisterAddressToken(value, metadata);
            }
            // Other address
            else if (value.StartsWith("?", StringComparison.Ordinal))
            {
                throw new NotImplementedException("? is a placeholder! Cannot use to address memory!");
            }
            // Label
            else if (value.StartsWith(":", StringComparison.Ordinal))
            {
                return new LabelDeclarationToken(value, metadata);
            }
            // Name
            else if (value.StartsWith("_", StringComparison.Ordinal))
            {
                return new NameToken(value, metadata);
            }
            // Compiler directive
            else if (value.StartsWith(".", StringComparison.Ordinal))
            {
                bool isDirective = Enum.TryParse(value.Substring(1).ToUpper(), out Directives directive);
                if (isDirective)
                {
                    return new DirectiveToken(directive, metadata);
                }
            }

            // Try to parse to an Instruction
            bool isInstruction = Enum.TryParse(value.ToUpper(), out Instructions instruction);
            if (isInstruction)
            {
                return new InstructionToken(instruction, metadata);
            }

            throw new NullReferenceException("Cannot create Token!");
        }
    }
}
