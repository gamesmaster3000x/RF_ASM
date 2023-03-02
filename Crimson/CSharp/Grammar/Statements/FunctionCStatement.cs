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
        public ScopeCToken Scope { get; }
        public override FullNameCToken Name { get => FunctionHeader.Identifier; set { FunctionHeader.Identifier = value; } }

        public FunctionCStatement(CrimsonTypeCToken returnType, Header header, ScopeCToken scope)
        {
            ReturnType = returnType;
            FunctionHeader = header;
            Scope = scope;
        }

        public override void Link(LinkingContext ctx)
        {
            if (IsLinked()) return;

            ReturnType.Link(ctx);
            ((ICrimsonToken)FunctionHeader).Link(ctx);
            Scope.Link(ctx);

            SetLinked(true);
        }

        public Fragment GetCrimsonBasic()
        {
            Fragment function = new Fragment(0);

            Fragment functionHead = new Fragment(0);
            functionHead.Add(new LabelBStatement(Name.ToString()));
            functionHead.Add(new PushSfBStatement());

            Fragment functionBody = new Fragment(1);
            functionBody.Add(Scope.GetCrimsonBasic());

            Fragment functionFoot = new Fragment(0);
            functionFoot.Add(new PushSfBStatement());
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
            public FullNameCToken Identifier { get; set; }

            public Parameter(CrimsonTypeCToken type, FullNameCToken identifier)
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
            public FullNameCToken Identifier { get; set; }
            public List<Parameter> Parameters { get; protected set; }

            public Header(FullNameCToken identifier, List<Parameter> parameters)
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
