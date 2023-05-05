using Compiler.Generalising;
using CrimsonCore.Linking;

namespace Compiler.Parsing.Statements
{
    /// <summary>
    /// A collection of ICrimsonTokens which make a coherent "phrase".
    /// </summary>
    public abstract class AbstractCrimsonStatement
    {
        public bool Linked { get; set; }

        public abstract void Link (LinkingContext context);

        public abstract IGeneralAssemblyStructure Generalise (GeneralisationContext context);

        public override string ToString ()
        {
            return GetType().Name;
        }
    }
}