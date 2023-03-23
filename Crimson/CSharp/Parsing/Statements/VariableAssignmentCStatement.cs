using Crimson.CSharp.Assembly;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using Crimson.CSharp.Linking;
using System;

namespace Crimson.CSharp.Grammar.Statements
{
    public class VariableAssignmentCStatement : AbstractCrimsonStatement
    {

        public FullNameCToken Name { get; set; }
        public SimpleValueCToken? Simple { get; }
        public ComplexValueCToken? Complex { get; }

        public VariableAssignmentCStatement (FullNameCToken identifier, SimpleValueCToken value)
        {
            Name = identifier;
            Simple = value;
        }

        public VariableAssignmentCStatement (FullNameCToken identifier, ComplexValueCToken value)
        {
            Name = identifier;
            Complex = value;
        }

        public override void Link (LinkingContext ctx)
        {
            Name = LinkerHelper.LinkIdentifier(Name, ctx);
            Simple?.Link(ctx);
            Complex?.Link(ctx);
            Linked = true;
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment result = new Fragment(0);

            if (Simple != null)
            {
                result.Add(new CommentBStatement(Simple.GetText()));
            }
            else if (Complex != null)
            {
                result.Add(Complex.GetBasicFragment());
                result.Add(new SetBStatement(Name.ToString(), -1, "VAR_ASSIGN_C_VAL"));
            }
            else
            {
                throw new FlatteningException($"No value to be assigned to variable {Name}");
            }

            return result;
        }
    }
}