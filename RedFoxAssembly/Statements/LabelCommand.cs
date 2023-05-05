using NLog;
using RedFoxAssembly.Core;
using RedFoxAssembly.Exceptions;

namespace RedFoxAssembly.Statements
{
    internal class LabelCommand : ICommand
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public string ID { get; protected set; }
        public int Position { get; set; }

        public LabelCommand (string id)
        {
            ID = id;
        }

        public byte[] GetBytes (RFASMCompiler compiler)
        {
            int targetWidth = compiler.Options!.DataWidth;
            byte[] positionBytes = BitConverter.GetBytes(Position).Reverse().ToArray();
            // if (positionBytes.Length < 1) throw new CompilationException($"Byte position {_position} of label {_id} is illegal! ( <0 )");
            // if (positionBytes.Length > targetWidth) throw new CompilationException($"Byte position {_position} of label {_id} lies outside the possible range for a program with data width {targetWidth}");

            byte[] result = CompilerUtils.FixArrayWidthPreservingBack(targetWidth, positionBytes);

            return result;
        }

        public int GetPredictedLength (RFASMCompiler compiler)
        {
            return 0;
        }

        internal void DeclareLabel (RFASMCompiler compiler, int position)
        {
            if (compiler.Labels.ContainsKey(ID)) throw new PreCompilationException($"Illegal duplicate declaration of label {ID}");
            Position = position;
            compiler.Labels.Add(ID, this);
        }
    }
}
