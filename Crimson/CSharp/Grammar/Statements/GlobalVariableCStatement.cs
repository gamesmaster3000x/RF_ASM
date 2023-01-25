﻿using Crimson.CSharp.Core;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Grammar.Statements
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    public class GlobalVariableCStatement : GlobalCStatement
    {
        private CrimsonTypeCToken type;

        public ComplexValueCToken? Complex { get; }
        public SimpleValueCToken? Simple { get; }

        public GlobalVariableCStatement(CrimsonTypeCToken type, string identifier, ComplexValueCToken value)
        {
            this.type = type;
            Name = identifier;
            Complex = value;
        }

        public GlobalVariableCStatement(CrimsonTypeCToken type, string identifier, SimpleValueCToken value)
        {
            this.type = type;
            Name = identifier;
            Simple = value;
        }

        public override void Link(LinkingContext ctx)
        {
            return;
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment statements = new Fragment(0);

            if (Value != null)
            {
                Fragment valueStatements = Value.GetBasicFragment();
                statements.Add(valueStatements);
            }

            statements.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, Name, type.GetByteSize().ToString()));
            statements.Add(new SetBStatement(Name, "GLO_VAR_ASSIGN_VAL"));

            return statements;
        }
    }
}
