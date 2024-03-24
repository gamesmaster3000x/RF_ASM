using Compiler.Generaliser;

namespace Compiler.Parser.Syntax
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public interface IAssemblable
    {
        public abstract IGeneralAssemblyStructure Generalise (GeneralisationContext context);
    }
}