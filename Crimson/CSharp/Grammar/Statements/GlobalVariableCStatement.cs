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
    public class GlobalVariableCStatement : INamedStatement
    {
        private CrimsonTypeCToken type;
        public FullNameCToken Name { get; protected set; }
        private bool _linked = false;

        public ComplexValueCToken? Complex { get; }
        public SimpleValueCToken? Simple { get; }

        public GlobalVariableCStatement(CrimsonTypeCToken type, FullNameCToken identifier, ComplexValueCToken value)
        {
            this.type = type;
            Name = identifier;
            Complex = value;
        }

        public GlobalVariableCStatement(CrimsonTypeCToken type, FullNameCToken identifier, SimpleValueCToken value)
        {
            this.type = type;
            Name = identifier;
            Simple = value;
        }

        public void Link(LinkingContext ctx)
        {
            return;
        }

        public bool IsLinked ()
        {
            return _linked;
        }

        public FullNameCToken GetName ()
        {
            return Name;
        }

        public void SetName (FullNameCToken name)
        {
            Name = name;
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment statements = new Fragment(0);

            // int i = (6 + 5);
            if (Complex != null)
            {
                Fragment valueStatements = Complex.GetBasicFragment();
                statements.Add(valueStatements);
                statements.Add(new IncSpBStatement(type.GetByteSize()));
                statements.Add(new SetBStatement(Name.ToString(), -1, valueStatements.ResultHolder!));

            }
            else if (Simple != null)
            {
                statements.Add(new IncSpBStatement(type.GetByteSize()));
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
