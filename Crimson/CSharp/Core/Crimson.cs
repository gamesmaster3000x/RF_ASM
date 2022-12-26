using CommandLine;
using NLog;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Statements;
using System.IO.Abstractions;

namespace Crimson.CSharp.Core
{
    internal class Crimson
    {

        private static Logger? LOGGER;

        static Task<int> Main(string[] args)
        {
            bool useAutowiredArgs = true;
            if (useAutowiredArgs)
            {
                Console.WriteLine("Using autowired (debug) program arguments");
                string testProgramsPath = "../../../Resources/Documentation/Examples/"; // Escape bin, Debug, and net6.0
                args = new string[] { 
                    "-s", testProgramsPath + "Main_Utils/main.crm", 
                    "-t", "out", 
                    "-n", "C:/Crimson/Native/" , 
                    "--rfasm"
                };
            }

            // Start
            Console.WriteLine("");
            Console.WriteLine(" ~C~ ");
            Console.WriteLine("Crimson Language Translator, by GenElectrovise, for GamesMaster3000X");
            Console.WriteLine("https://github.com/gamesmaster3000x/RF_ASM");
            Console.WriteLine(" ~C~ ");
            Console.WriteLine("");

            Console.WriteLine("Parsing options");
            return CommandLine.Parser.Default.ParseArguments<CrimsonOptions>(args).MapResult((options) =>
            {
                Console.WriteLine("  Option: CompilationSourcePath: " + options.TranslationSourcePath);
                Console.WriteLine("  Option: CompilationTargetPath: " + options.TranslationTargetPath);
                Console.WriteLine("  Option: NativeLibraryPath: " + options.NativeLibraryPath);
                Console.WriteLine("  Option: CleanFiles: " + options.CleanFiles);
                Console.WriteLine("  Option: (Platform) CrimsonBasic: " + options.CrimsonBasic);
                Console.WriteLine("  Option: (Platform) RFASM: " + options.RFASM);

                ConfigureNLog();

                UnitGenerator generator = new UnitGenerator(options);
                Linker linker = new Linker(options, generator);
                CrimsonTranslator compiler = new CrimsonTranslator(options, generator, linker);

                return Task.FromResult(compiler.FullyCompileFromOptions());
            },
            (error) =>
            {
                Console.Error.WriteLine("An issue occurred while parsing the program arguments (invalid arguments)");
                return Task.FromResult(-1);
            });
        }

        private static void ConfigureNLog()
        {
            Console.WriteLine("Configuring NLog...");
            NLog.Config.LoggingConfiguration config = new NLog.Config.LoggingConfiguration();
            var fileTarget = new NLog.Targets.FileTarget("CrimsonFileLogTarget")
            {
                FileName = "Crimson_${shortdate}.log",
                Layout = "${level} | ${time} | ${logger} > ${message:withexception=true}",
                DeleteOldFileOnStartup = true
            };
            var consoleTarget = new NLog.Targets.ConsoleTarget("CrimsonConsoleLogTarget")
            {
                Layout = "${level} | ${time} | ${logger} > ${message:withexception=true}"
            };
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);
            LogManager.Configuration = config;
            Console.WriteLine("NLog configured!");

            LOGGER = LogManager.GetCurrentClassLogger();
            Console.WriteLine("Testing TRACE and FATAL level logging...");
            LOGGER.Trace("Testing trace level logging...");
            LOGGER.Fatal("Testing fatal level logging...");
            Console.WriteLine("Did you see the TRACE and FATAL test messages?");
        }
    }
}