using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class RFASMProgram
    {
        public List<IConfiguration> Configuations { get; }
        public List<ICommand> Commands { get; }

        public RFASMProgram(): this(new List<IConfiguration>(), new List<ICommand>())
        {
        }

        public RFASMProgram(List<IConfiguration> configurations, List<ICommand> commands)
        {
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
