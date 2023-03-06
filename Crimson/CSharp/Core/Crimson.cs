using CommandLine;
using NLog;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using System.IO.Abstractions;

namespace Crimson.CSharp.Core
{
    internal class Crimson
    {

        private static Logger? LOGGER;
        internal static readonly string VERSION = "v0.0";
        public static CrimsonOptions Options { get; private set; }
        public static CrimsonCompiler Compiler { get; private set; }

        public const int ERROR_UNKNOWN = -100;

        static int Main(string[] args)
        {
            bool useAutowiredArgs = true;
            if (useAutowiredArgs)
            {
                Console.WriteLine("Using autowired (debug) program arguments");
                string resourcesPath = "../../../Resources/"; // Escape bin, Debug, and net6.0
                args = new string[] {
                    "-s", resourcesPath + "Test Compilations/main.crm",
                    "-t", resourcesPath + "Test Compilations/result/main",
                    "-n", resourcesPath + "Native Library/" ,
                    "--rfasm"
                };
            }

            // Start
            ShowSplash();
            ShowCredits();

            Console.WriteLine("Parsing Crimson options");
            Options = CommandLine.Parser.Default.ParseArguments<CrimsonOptions>(args).Value;

            Console.WriteLine("  Option: CompilationSourcePath: " + Options.TranslationSourcePath);
            Console.WriteLine("  Option: CompilationTargetPath: " + Options.TranslationTargetPath);
            Console.WriteLine("  Option: NativeLibraryPath: " + Options.NativeLibraryPath);
            Console.WriteLine("  Option: DumpIntermediates: " + Options.DumpIntermediates);
            Console.WriteLine("  Option: (Platform) CrimsonBasic: " + Options.CrimsonBasic);
            Console.WriteLine("  Option: (Platform) RFASM: " + Options.RFASM);

            ConfigureNLog();

            Library generator = new Library(Options);
            Linker linker = new Linker(Options);
            Flattener flattener = new Flattener(Options);
            Compiler = new CrimsonCompiler(Options, generator, linker, flattener);

            try
            {
                return Compiler.FullyCompileFromOptions();
            } catch (System.Exception e)
            {
                Console.WriteLine(e);
                LOGGER!.Error(e);
                return ERROR_UNKNOWN;
            }
        }

        private static void ShowSplash()
        {
            Console.WriteLine("");
            Console.WriteLine("Crimson Language Compiler, by GenElectrovise, for GamesMaster3000X");
            Console.WriteLine("   _____          _                                   ");
            Console.WriteLine("  / ____|        (_)                                  ");
            Console.WriteLine(" | |       _ __   _   _ __ ___    ___    ___    _ __  ");
            Console.WriteLine(" | |      | '__| | | | '_ ` _ \\  / __|  / _ \\  | '_ \\ ");
            Console.WriteLine(" | |____  | |    | | | | | | | | \\__ \\ | (_) | | | | |");
            Console.WriteLine("  \\_____| |_|    |_| |_| |_| |_| |___/  \\___/  |_| |_|");
            Console.WriteLine("                                                      ");
            Console.WriteLine("https://github.com/gamesmaster3000x/RF_ASM");
            Console.WriteLine("");
        }

        private static void ShowCredits()
        {
            Console.WriteLine("");
            Console.WriteLine("  -> C R E D I T S <-  ");
            Console.WriteLine("Created by GenElectrovise https://github.com/GenElectrovise on behalf of GamesMaster3000X https://github.com/gamesmaster3000x");
            Console.WriteLine("ASCII art generated with https://patorjk.com/software/taag/");
            Console.WriteLine("ASCII art font 'Big' by Glenn Chappell, Bruce Jakeway and Paul Burton");
            Console.WriteLine("Using NuGet packages: ANTLR4, CommandLineParser, System.IO.Abstractions and NLog");
            Console.WriteLine("Written using Visual Studio in C#/.NET 6.0 (LTS)");
            Console.WriteLine("  -> - - - - - - - <-  ");
            Console.WriteLine("");
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