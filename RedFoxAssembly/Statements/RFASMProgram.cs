using NLog;
using RedFoxAssembly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedFoxAssembly.Statements
{
    internal class RFASMProgram
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public ProgramMetadata Metadata { get; protected set; }
        public List<IConfiguration> Configuations { get; protected set; }
        public List<ICommand> Commands { get; protected set; }

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

        internal void ReloadSerialMetadata(string metadataJson)
        {
            LOGGER.Info("Reloading RFASM program metadata...");

            try
            {
                ProgramMetadata? meta = JsonSerializer.Deserialize<ProgramMetadata>(metadataJson);
                if (meta == null)
                    return;
                Metadata = meta;
                LOGGER.Info($"Reloaded RFASM program metadata: {Metadata}");

            }
            catch (Exception e)
            {
                throw new ArgumentNullException("Error deserialising RFASM program metadata!", e);
            }
        }
    }
}
