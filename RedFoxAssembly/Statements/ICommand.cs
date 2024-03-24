using RedFoxAssembly.Core;

namespace RedFoxAssembly.Statements
{
    internal interface ICommand
    {
        int GetPredictedLength(RFASMCompiler compiler);

        byte[] GetBytes(RFASMCompiler compiler);
    }
}
