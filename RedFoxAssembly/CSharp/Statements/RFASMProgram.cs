using RedFoxAssembly.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class RFASMProgram
    {
        public ProgramMetadata Metadata { get; protected set; }
        public List<IConfiguration> Configuations { get; protected set; }
        public List<ICommand> Commands { get; protected set; }

        public RFASMProgram() : this(new ProgramMetadata(), new List<IConfiguration>(), new List<ICommand>())
        {
        }

        public RFASMProgram(ProgramMetadata metadata, List<IConfiguration> configurations, List<ICommand> commands)
        {
            Metadata = metadata;
            Configuations = configurations;
            Commands = commands;
        }

        internal void AddCommand(ICommand c)
        {
            Commands.Add(c);
        }

        internal void AddConfiguration(IConfiguration c)
        {
            Configuations.Add(c);
        }
    }
}
