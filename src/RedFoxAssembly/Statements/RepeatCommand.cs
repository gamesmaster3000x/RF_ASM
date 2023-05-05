using NLog;
using RedFoxAssembly.Core;
using RedFoxAssembly.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.Statements
{
    internal class RepeatCommand : ICommand
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public int Times { get; protected set; }
        public byte[] Contents { get; protected set; }

        public RepeatCommand (int times, List<RByte> rBytes) : this(times, RByte.MakeByteArr(rBytes)) { }

        public RepeatCommand (int times, List<Word> words) : this(times, Word.MakeByteArr(words)) { }

        public RepeatCommand (int times, byte[] contents)
        {
            if (times < 1) throw new ParsingException($"Cannot repeat less than once (recieved {times})");
            if (contents == null) throw new ParsingException("Cannot repeat null byte array");
            if (contents.Length < 0) throw new ParsingException("Cannot repeat 0-length byte array");

            Times = times;
            Contents = contents;
        }

        public byte[] GetBytes (RFASMCompiler compiler)
        {
            byte[] result = new byte[GetPredictedLength(compiler)];

            for (int i = 0; i < Times; i++)
                Array.Copy(Contents, 0, result, i * Contents.Length, Contents.Length);

            return result;
        }

        public int GetPredictedLength (RFASMCompiler compiler)
        {
            return Contents.Length * Times;
        }
    }
}
