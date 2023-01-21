using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class LabelCommand: ICommand
    {
        private string _id;
        private int? _position;

        public LabelCommand(string id)
        {
            _id = id;
        }

        public byte[] GetBytes(RFASMCompiler compiler)
        {
            return new byte[0];
        }

        internal void DeclareLabel(RFASMCompiler compiler, int position)
        {
            if (compiler.Labels.ContainsKey(_id)) throw new PreCompilationException($"Illegal duplicate declaration of label {_id}");
            _position = position;
            compiler.Labels.Add(_id, this);
        }
    }
}
