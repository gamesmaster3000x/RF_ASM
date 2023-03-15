﻿using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;

namespace Crimson.CSharp.Grammar.Statements
{
    internal class AssemblyCallCStatement : AbstractCrimsonStatement
    {
        private string assemblyText;

        public AssemblyCallCStatement (string assemblyText)
        {
            this.assemblyText = assemblyText;
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment f = new Fragment(0);
            f.Add(new AssemblyBStatement(assemblyText));
            return f;
        }

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }
    }
}