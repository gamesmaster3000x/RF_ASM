using Crimson.CSharp.Core;
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

            if (Simple != null)
            {
                result.Add(new CommentBStatement(Simple.GetText()));
            } 
            else if (Complex != null)
            {
                result.Add(Complex.GetBasicFragment());
                result.Add(new SetBStatement(Identifier.ToString(), -1, "VAR_ASSIGN_C_VAL"));
            } 
            else
            {
                throw new FlatteningException($"No value to be assigned to variable {Identifier}");
            }

            return result;
        }
    }
}