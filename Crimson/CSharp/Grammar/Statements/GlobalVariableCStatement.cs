using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
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
    public class GlobalVariableCStatement : AbstractCrimsonStatement, INamed
    {
        public FullNameCToken Name { get; protected set; }

        public ComplexValueCToken? Complex { get; }
        public SimpleValueCToken? Simple { get; }

        public GlobalVariableCStatement (FullNameCToken identifier, ComplexValueCToken value)
        {
            Name = identifier;
            Complex = value;
        }

        public GlobalVariableCStatement (FullNameCToken identifier, SimpleValueCToken value)
        {
            Name = identifier;
            Simple = value;
        }

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }

        public FullNameCToken GetName ()
        {
            return Name;
        }

        public void SetName (FullNameCToken name)
        {
            Name = name;
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment statements = new Fragment(0);

            // int i = (6 + 5);
            if (Complex != null)
            {
                Fragment valueStatements = Complex.GetBasicFragment();
                statements.Add(valueStatements);
                statements.Add(new SetBStatement(Name.ToString(), -1, valueStatements.ResultHolder!));

            }
            else if (Simple != null)
            {
                statements.Add(new SetBStatement(Name.ToString(), -1, Simple.GetText()));
            }
            else
            {
                throw new FlatteningException("Unable to flatten internal variable with no simple or complex value");
            }

            return statements;
        }
    }
}
