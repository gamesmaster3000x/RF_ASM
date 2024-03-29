﻿using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Grammar.Tokens
{
    /// <summary>
    /// ICrimsonStatements are made up of ICrimsonTokens. An ICrimsonToken cannot stand on its own.
    /// </summary>
    public interface ICrimsonToken
    {
        public void Link(LinkingContext ctx);
    }
}