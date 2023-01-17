using Crimson.CSharp.Core;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Core.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Statements
{
    /// <summary>
    /// A function, defined with the function keyword. Is a member of a package.
    /// </summary>
    public class FunctionCStatement : GlobalCStatement
    {

        public FunctionCStatement(CrimsonTypeCToken returnType, string name, IList<Parameter> parameters, IList<InternalStatement> statements)
        {
            ReturnType = returnType;
            Name = name;
            Parameters = parameters;
            Statements = statements;
        }

        public CrimsonTypeCToken ReturnType { get; }
        public IList<Parameter> Parameters { get; }
        public IList<InternalStatement> Statements { get; }

        public override void Link(LinkingContext ctx)
        {
            if (IsLinked()) return;

            foreach (var p in Parameters)
            {
                p.Link(ctx);
            }

            foreach (var s in Statements)
            {
                s.Link(ctx);
            }

            SetLinked(true);
        }

        public class Parameter
        {
            public CrimsonTypeCToken Type { get; }
            public string Identifier { get; set;  }

            public Parameter(CrimsonTypeCToken type, string identifier)
            {
                Type = type;
                Identifier = identifier;
            }

            internal void Link(LinkingContext ctx)
            {
                Type.Link(ctx);
                Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            }
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
    }
}
