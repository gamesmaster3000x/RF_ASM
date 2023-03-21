using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System.Drawing;
using System.Xml.Linq;
using static Crimson.CSharp.Grammar.Tokens.Comparator;

namespace Crimson.CSharp.Grammar.Statements
{
    public class ScopeVariableCStatement : AbstractCrimsonStatement
    {
        public SimpleValueCToken Size { get; set; }
        public FullNameCToken Identifier { get; private set; }

        public ScopeVariableCStatement (FullNameCToken identifier, SimpleValueCToken size)
        {
            Size = size;
            Identifier = identifier;

            if (identifier == null) throw new CrimsonParserException("Null identifier");
            if (identifier.HasLibrary()) throw new CrimsonParserException($"Identifier {identifier} for internal variable may not contain a library name.");
            if (!identifier.HasMember()) throw new CrimsonParserException($"Identifier {identifier} for internal variable must have a member name.");
        }

        public override void Link (LinkingContext ctx)
        {
            Size.Link(ctx);
            Identifier.Link(ctx);
            Linked = true;
        }

        public override Fragment GetCrimsonBasic ()
        {
            Fragment statements = new Fragment(0);

            statements.Add(new CommentBStatement($"Declare {Identifier}"));
            statements.Add(new CommentBStatement($"IncSp {Identifier}"));

            return statements;
        }

        public bool IsLinked ()
        {
            throw new NotImplementedException();
        }
    }
}