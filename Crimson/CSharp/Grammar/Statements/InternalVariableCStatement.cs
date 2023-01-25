using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Xml.Linq;
using static Crimson.CSharp.Grammar.Tokens.Comparator;

namespace Crimson.CSharp.Grammar.Statements
{
    public class InternalVariableCStatement : InternalStatement
    {
        private CrimsonTypeCToken type;
        public string Identifier { get; private set; }

        public ComplexValueCToken? Complex { get; }
        public SimpleValueCToken? Simple { get; }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier, SimpleValueCToken simple)
        {
            this.type = type;
            this.Identifier = identifier;
            Simple = simple;

            if (identifier == null) throw new CrimsonParserException("Null identifier");
            if (type == null) throw new CrimsonParserException($"Null type for variable {identifier}");
            if (Simple == null) throw new CrimsonParserException($"Must assign initial (declaration) value to variable {identifier}");
        }

        public InternalVariableCStatement(CrimsonTypeCToken type, string identifier, ComplexValueCToken complex)
        {
            this.type = type;
            this.Identifier = identifier;
            Complex = complex;

            if (identifier == null) throw new CrimsonParserException("Null identifier");
            if (type == null) throw new CrimsonParserException($"Null type for variable {identifier}");
            if (Complex == null) throw new CrimsonParserException($"Must assign initial (declaration) value to variable {identifier}");
        }

        public override void Link(LinkingContext ctx)
        {
            // Only run if not null
            Simple?.Link(ctx);
            Complex?.Link(ctx);
            return;
        }

        public override Fragment GetCrimsonBasic()
        {
            Fragment statements = new Fragment(0);

            // int i = (6 + 5);
            if (Complex != null)
            {
                Fragment valueStatements = Complex.GetBasicFragment();
                statements.Add(valueStatements);
                statements.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, Identifier, type.GetByteSize().ToString()));
                statements.Add(new SetBStatement(Identifier, valueStatements.ResultHolder!));

            } 
            else if (Simple != null)
            {
                statements.Add(new StackBStatement(StackBStatement.StackOperation.ALLOCATE, Identifier, type.GetByteSize().ToString()));
                statements.Add(new SetBStatement(Identifier, Simple.GetText()));
            } 
            else
            {
                throw new FlatteningException("Unable to flatten internal variable with no simple or complex value");
            }

            return statements;
        }
    }
}