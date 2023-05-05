﻿using Compiler.Generalising.Structures;
using Compiler.Mapping;
using Compiler.Parsing.Tokens.Values;
using Compiler.Generalising;

namespace Compiler.Parsing.Statements
{
    internal class ReturnCStatement : AbstractCrimsonStatement
    {
        public ReturnCStatement (SimpleValueCToken value)
        {
            Value = value;
        }

        public SimpleValueCToken Value { get; }

        public override void Link (MappingContext ctx)
        {
            Mapped = true;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            return new ReturnAssemblyStructure();
        }
    }
}