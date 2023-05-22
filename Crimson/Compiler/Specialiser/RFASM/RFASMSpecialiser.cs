using NLog;
using Compiler.Generaliser;

namespace Compiler.Specialiser.RFASM
{
    internal class RFASMSpecialiser : ISpecialiser
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public AbstractSpecificAssemblyProgram Specialise (GeneralAssemblyProgram general)
        {
            RFASMProgram program = new RFASMProgram();

            foreach (var s in general.Structures)
                program.Add(new RFASMComment(s.ToString()));

            return program;
        }
    }
}
