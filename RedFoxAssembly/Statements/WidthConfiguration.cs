using RedFoxAssembly.Core;

namespace RedFoxAssembly.Statements
{
    internal class WidthConfiguration : IConfiguration
    {
        private int _width;

        public WidthConfiguration(int width)
        {
            _width = width;
        }

        void IConfiguration.Resolve(RFASMCompiler compiler)
        {
            compiler.Options!.DataWidth = _width;
        }
    }
}
