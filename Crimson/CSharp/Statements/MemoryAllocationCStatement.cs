﻿using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;

namespace Crimson.CSharp.Statements
{
    internal class MemoryAllocationCStatement : InternalStatement
    {
        private string identifier;
        private int number;

        public MemoryAllocationCStatement(string identifier, int number)
        {
            this.identifier = identifier;
            this.number = number;
        }

        public override void Link(LinkingContext ctx)
        {
            return;
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment crimsonBasic = new Fragment(0);

            crimsonBasic.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, identifier, number.ToString()));

            return crimsonBasic;
        }
    }
}