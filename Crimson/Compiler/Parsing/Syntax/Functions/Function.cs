
using Compiler.Mapping;
using Compiler.Generalising.Structures;
using Compiler.Generalising;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.Syntax.Functions
{
    /// <summary>
    /// A function, defined with the function keyword. Is a member of a package.
    /// </summary>
    public class Function : IAssemblable, INamed, IHasScope, IMappable
    {
        public Header FunctionHeader { get; }
        public Scope Scope { get; }
        public FullName Name { get => FunctionHeader.Identifier; set { FunctionHeader.Identifier = value; } }

        public Function (Header header, Scope scope)
        {
            FunctionHeader = header;
            Scope = scope;

            Scope.Name = Name.ToString();
        }

        public void Map (MappingContext ctx)
        {
            Name = ctx.GetUniqueFunctionName(Name);

            FunctionHeader.Map(ctx);
            Scope.Map(ctx);
        }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            SubroutineAssemblyStructure subroutine = new SubroutineAssemblyStructure(context, Name.ToString());
            context.EnterScope();

            subroutine.AddSubStructure(Scope.Generalise(context));

            context.LeaveScope();
            return subroutine;
        }

        public FullName GetName ()
        {
            return Name;
        }

        public void SetName (FullName name)
        {
            Name = name;
        }

        public Scope GetScope ()
        {
            return Scope;
        }

        public class Parameter : IMappable
        {
            public ISimpleValue Size { get; set; }
            public FullName Identifier { get; set; }

            public Parameter (ISimpleValue size, FullName identifier)
            {
                Size = size;
                Identifier = identifier;
            }

            public void Map (MappingContext ctx)
            {
                (Size as IMappable)?.Map(ctx);
                Identifier.Map(ctx);
            }
        }

        public class Header : IMappable
        {
            public FullName Identifier { get; set; }
            public List<Parameter> Parameters { get; protected set; }

            public Header (FullName identifier, List<Parameter> parameters)
            {
                Identifier = identifier;
                Parameters = parameters;
            }

            public void Map (MappingContext ctx)
            {
                Identifier.Map(ctx);
                foreach (var p in Parameters)
                    p.Map(ctx);
            }
        }
    }
}
