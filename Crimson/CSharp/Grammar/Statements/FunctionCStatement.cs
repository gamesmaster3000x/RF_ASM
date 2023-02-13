using Crimson.CSharp.Core;
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
    /// A function, defined with the function keyword. Is a member of a package.
    /// </summary>
    public class FunctionCStatement : GlobalCStatement
    {


        public CrimsonTypeCToken ReturnType { get; }
        public Header FunctionHeader { get; }
        public IList<InternalStatement> Statements { get; }
        public override string Name { get => FunctionHeader.Identifier; }

        public FunctionCStatement(CrimsonTypeCToken returnType, Header header, IList<InternalStatement> statements)
        {
            ReturnType = returnType;
            FunctionHeader = header;
            Statements = statements;
        }

        public override void Link(LinkingContext ctx)
        {
            if (IsLinked()) return;

            ReturnType.Link(ctx);
            ((ICrimsonToken)FunctionHeader).Link(ctx);

            foreach (var s in Statements)
            {
                s.Link(ctx);
            }

            SetLinked(true);
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment function = new Fragment(0);

            Fragment functionHead = new Fragment(0);
            functionHead.Add(new LabelBStatement(Name));
            functionHead.Add(new StackBStatement(StackBStatement.StackOperation.PUSH_FRAME));

            Fragment functionBody = new Fragment(1);
            foreach (var s in Statements)
            {
                functionBody.Add(s.GetCrimsonBasic());
            }

            Fragment functionFoot = new Fragment(0);
            functionFoot.Add(new StackBStatement(StackBStatement.StackOperation.POP_FRAME));
            functionFoot.Add(new ReturnBStatement());
            functionFoot.Add(new CommentBStatement(""));

            function.Add(functionHead);
            function.Add(functionBody);
            function.Add(functionFoot);

            return function;
        }

        public class Parameter : ICrimsonToken
        {
            public CrimsonTypeCToken Type { get; }
            public string Identifier { get; set; }

            public Parameter(CrimsonTypeCToken type, string identifier)
            {
                Type = type;
                Identifier = identifier;
            }

            void ICrimsonToken.Link(LinkingContext ctx)
            {
                Type.Link(ctx);
                Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            }
        }

        public class Header : ICrimsonToken
        {
            public string Identifier { get; protected set; }
            public List<Parameter> Parameters { get; protected set; }

            public Header(string identifier, List<Parameter> parameters)
            {
                Identifier = identifier;
                Parameters = parameters;
            }

            void ICrimsonToken.Link(LinkingContext ctx)
            {
                Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
                foreach (var p in Parameters)
                {
                    ((ICrimsonToken)p).Link(ctx);
                }
            }
        }
    }
}
