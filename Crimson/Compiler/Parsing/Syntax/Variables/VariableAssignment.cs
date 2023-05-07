using Compiler.Mapping;
using Compiler.Parsing.Syntax.Values;

namespace Compiler.Parsing.Syntax.Variables
{
    public class VariableAssignment : IStatement, IMappable
    {

        public FullName Name { get; set; }
        public ISimpleValue Size { get; set; }
        public ISimpleValue? Simple { get; }
        public IComplexValue? Complex { get; }

        public VariableAssignment (FullName identifier, ISimpleValue size, ISimpleValue value)
        {
            Name = identifier;
            Size = size;
            Simple = value;
        }

        public VariableAssignment (FullName identifier, ISimpleValue size, IComplexValue value)
        {
            Name = identifier;
            Size = size;
            Complex = value;
        }

        public void Map (MappingContext ctx)
        {
            Name.Map(ctx);
            Simple?.Map(ctx);
            Complex?.Map(ctx);
        }

        internal bool IsKnownAtCompileTime ()
        {
            throw new NotImplementedException();
        }

        public string GetText ()
        {
            throw new NotImplementedException();
        }
    }
}