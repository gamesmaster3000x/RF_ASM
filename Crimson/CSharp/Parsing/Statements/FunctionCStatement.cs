using Crimson.CSharp.Generalising;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Tokens;
using Crimson.CSharp.Specialising;

namespace Crimson.CSharp.Parsing.Statements
{
    /// <summary>
    /// A function, defined with the function keyword. Is a member of a package.
    /// </summary>
    public class FunctionCStatement : AbstractCrimsonStatement, INamed, IHasScope
    {
        public Header FunctionHeader { get; }
        public Scope Scope { get; }
        public FullNameCToken Name { get => FunctionHeader.Identifier; set { FunctionHeader.Identifier = value; } }

        public FunctionCStatement (Header header, Scope scope)
        {
            FunctionHeader = header;
            Scope = scope;

            Scope.Name = Name.ToString();
        }

        public override void Link (LinkingContext ctx)
        {
            if (Linked) return;

            ((ICrimsonToken) FunctionHeader).Link(ctx);
            Scope.Link(ctx);

            Linked = true;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            SubroutineAssemblyStructure subroutine = new SubroutineAssemblyStructure(context, Name.ToString());
            context.EnterScope();

            subroutine.AddSubStructure(Scope.Generalise(context));

            context.LeaveScope();
            return subroutine;
        }

        public FullNameCToken GetName ()
        {
            return Name;
        }

        public void SetName (FullNameCToken name)
        {
            Name = name;
        }

        public Scope GetScope ()
        {
            return Scope;
        }

        public class Parameter : ICrimsonToken
        {
            public SimpleValueCToken Size { get; set; }
            public FullNameCToken Identifier { get; set; }

            public Parameter (SimpleValueCToken size, FullNameCToken identifier)
            {
                Size = size;
                Identifier = identifier;
            }

            void ICrimsonToken.Link (LinkingContext ctx)
            {
                Size.Link(ctx);
                Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
            }
        }

        public class Header : ICrimsonToken
        {
            public FullNameCToken Identifier { get; set; }
            public List<Parameter> Parameters { get; protected set; }

            public Header (FullNameCToken identifier, List<Parameter> parameters)
            {
                Identifier = identifier;
                Parameters = parameters;
            }

            void ICrimsonToken.Link (LinkingContext ctx)
            {
                Identifier = LinkerHelper.LinkIdentifier(Identifier, ctx);
                foreach (var p in Parameters)
                    ((ICrimsonToken) p).Link(ctx);
            }
        }
    }
}
