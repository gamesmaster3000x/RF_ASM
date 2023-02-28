﻿using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class VariableAssignmentCStatement : InternalStatement
    {

        public FullNameCToken Identifier { get; set; }
        public SimpleValueCToken? Simple { get; }
        public ComplexValueCToken? Complex { get; }

        public VariableAssignmentCStatement(FullNameCToken identifier, SimpleValueCToken value)
        {
            Identifier = identifier;
            Simple = value;
        }

        public VariableAssignmentCStatement(FullNameCToken identifier, ComplexValueCToken value)
        {
            Identifier = identifier;
            Complex = value;
        }

        public override void Link(LinkingContext ctx)
        {
            Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            Simple?.Link(ctx);
            Complex?.Link(ctx);
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment result = new Fragment(0);

            if (Complex == null) throw new FlatteningException("Illegal value assignment to " + Identifier + " (Proposed value is compiler-null)");
            result.Add(Complex.GetBasicFragment());
            result.Add(new SetBStatement(Identifier.ToString(), "VAR_ASSIGN_C_VAL"));

            return result;
        }
    }
}