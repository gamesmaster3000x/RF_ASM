using Compiler.Generalising.Structures;
using Compiler.Mapper;
using Compiler.Generaliser;

namespace Compiler.Parser.Syntax.Variables
{
    public class Mask : IAssemblable, INamed, IMappable
    {
        public FullName Name { get; set; }

        public Dictionary<string, int> values { get; set; }

        public Mask (FullName name, IList<IAssemblable> body)
        {
            Name = name;
            Body = body;
        }

        public IList<IAssemblable> Body { get; }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            return new EmptyAssemblyStructure();
        }

        public void Map (MappingContext ctx)
        {
            Name = ctx.GetUniqueFunctionName(Name);
        }

        public FullName GetName ()
        {
            return Name;
        }

        public void SetName (FullName name)
        {
            Name = name;
        }

        public int GetSize ()
        {
            return -10000;
        }
    }
}