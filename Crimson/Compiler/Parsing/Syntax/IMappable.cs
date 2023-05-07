using Compiler.Mapping;

namespace Compiler.Parsing.Syntax
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface IMappable
    {
        void Map (MappingContext context);
    }
}