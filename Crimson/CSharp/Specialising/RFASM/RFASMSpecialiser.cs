using Crimson.CSharp.Generalising;
using NLog;

namespace Crimson.CSharp.Specialising.RFASM
{
    internal class RFASMSpecialiser : ISpecialiser
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public AbstractSpecificAssemblyProgram Specialise (GeneralAssemblyProgram general)
        {
            throw new NotImplementedException();
        }
    }
}
