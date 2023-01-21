using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Statements
{
    internal class RFASMProgram
    {
        private List<IConfiguration> _configurations;
        private List<ICommand> _commands;

        public RFASMProgram(): this(new List<IConfiguration>(), new List<ICommand>())
        {
        }

        public RFASMProgram(List<IConfiguration> configurations, List<ICommand> commands)
        {
            _configurations = configurations;
            _commands = commands;
        }

        internal void AddCommand(ICommand c)
        {
            _commands.Add(c);
        }

        internal void AddConfiguration(IConfiguration c)
        {
            _configurations.Add(c);
        }
    }
}
