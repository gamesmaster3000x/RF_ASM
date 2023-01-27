using NLog;
using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class LabelCommand: ICommand
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private string _id;
        private int _position;

        public LabelCommand(string id)
        {
            _id = id;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            int targetWidth = compiler.meta!.DataWidth;
            byte[] positionBytes = BitConverter.GetBytes(_position).Reverse().ToArray();
            // if (positionBytes.Length < 1) throw new CompilationException($"Byte position {_position} of label {_id} is illegal! ( <0 )");
            // if (positionBytes.Length > targetWidth) throw new CompilationException($"Byte position {_position} of label {_id} lies outside the possible range for a program with data width {targetWidth}");

            byte[] result = CompilerUtils.FixArrayWidthPreservingBack(targetWidth, positionBytes);

            return result;
        }

        public int GetPredictedLength(RFASMCompiler compiler)
        {
            return 0;
        }

        internal void DeclareLabel(RFASMCompiler compiler, int position)
        {
            if (compiler.Labels.ContainsKey(_id)) throw new PreCompilationException($"Illegal duplicate declaration of label {_id}");
            _position = position;
            compiler.Labels.Add(_id, this);
        }
    }
}
