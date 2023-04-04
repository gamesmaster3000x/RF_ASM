using NLog;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Specialising;
using Crimson.CSharp.Specialising.RFASM;
using Crimson.CSharp.Generalising;
using Crimson.CSharp.Exceptions;

namespace Crimson.CSharp.Core
{
    internal class Crimson
    {

        private static Logger? LOGGER;
        internal static readonly string VERSION = "v0.0";
        public static CrimsonOptions Options { get; private set; }
        public static CrimsonCompiler Compiler { get; private set; }

        public const int ERROR_UNKNOWN = -100;

        public static int Main (string[] args)
        {
            Thread.CurrentThread.Name = "main";

            // Start
            ShowSplash();
            ShowCredits();

            // Setup arguments
            string CombineAndFormatPath (string a, string b) => Path.GetFullPath(Path.Combine(a, b));
            bool useAutowiredArgs = true;
            if (useAutowiredArgs)
            {
                Console.WriteLine("Using autowired (debug) program arguments");
                string resourcesPath = "Resources/"; // Escape bin, Debug, and net6.0

                args = new string[] {
                    $"-s {CombineAndFormatPath(resourcesPath, "Test Compilations/main.crm")}",
                    $"-t {CombineAndFormatPath(resourcesPath, "Test Compilations/result/main")}",
                    $"-n {CombineAndFormatPath(resourcesPath, "Native Library/")}",
                    "-w", "4",
                    "--rfasm"
                };

                Console.WriteLine(String.Join(' ', args));
            }

            Console.WriteLine("Parsing Crimson options");
            Options = CommandLine.Parser.Default.ParseArguments<CrimsonOptions>(args).Value;
            if (Options == null) throw new NullReferenceException($"Unable to parse arguments {args}");

            Console.WriteLine("  Option: SourceUri: " + Options.SourceUri);
            Console.WriteLine("  Option: TargetUri: " + Options.TargetUri);
            Console.WriteLine("  Option: NativeUri: " + Options.NativeUri);
            Console.WriteLine("  Option: DumpIntermediates: " + Options.DumpIntermediates);
            Console.WriteLine("  Option: DataWidth: " + Options.DataWidth);
            Console.WriteLine("  Option: (Platform) CrimsonBasic: " + Options.CrimsonBasic);
            Console.WriteLine("  Option: (Platform) RFASM: " + Options.RFASM);

            //
            ConfigureNLog();

            // 
            Library generator = new Library();
            Linker linker = new Linker(Options);
            Generaliser generaliser = new Generaliser();
            ISpecialiser specialiser = new RFASMSpecialiser(); //TODO Don't default to RFASM
            Compiler = new CrimsonCompiler(Options, generator, linker, generaliser, specialiser);

            try
            {
                Compiler.FullyCompileFromOptions();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                LOGGER!.Error(e);
                return ERROR_UNKNOWN;
            }
            return 0;
        }

        private static void ShowSplash ()
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

        private static void ShowCredits ()
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

        private static void ConfigureNLog ()
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
                Layout = "${level:uppercase=true:padding=-10} | ${time} | ${threadname:whenEmpty=${threadid}:padding=-5} | ${logger} > ${message:withexception=true}"
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

        private static readonly object panicLock = new object();
        public static void Panic (CrimsonException reason)
        {
            // Prevent multi access (keep logs clean)
            lock (panicLock)
            {
                List<string> lines = new List<string>
                {
                    "",
                    " ### COMPILER PANIC!! ###"
                };
                lines.AddRange(reason.GetDetailedMessage());
                lines.Add($"Panic code: {(int) reason.Code}");

                lines.ForEach(line => LOGGER!.Error(line));
                lines.ForEach(line => Console.Error.WriteLine(line));
                Environment.Exit((int) reason.Code);
            }
        }

        public static void Panic (string message, PanicCode code, Exception e)
        {
            if (e is CrimsonException ce)
            {
                Panic(ce);
            }
            else
            {
                ExceptionCrimsonException reason = new ExceptionCrimsonException(message, code, e);
                Panic(reason);
            }

            // Exits before here
            Environment.Exit((int) code);
        }

        public enum PanicCode
        {
            OK = 0,

            PARSE = -100,
            PARSE_STATEMENT = -110,
            PARSE_SCOPE = -120,
            PARSE_URI = -130,

            LINK = -200,
            GENERALISE = -300,
            SPECIALISE = -400,
        }
    }
}