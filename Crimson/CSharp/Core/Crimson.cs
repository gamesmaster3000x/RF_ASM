using NLog;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Specialising;
using Crimson.CSharp.Specialising.RFASM;
using Crimson.CSharp.Generalising;
using Crimson.CSharp.Exceptions;
using System.Globalization;

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

        public static void Panic (string message, PanicCode code, Exception e)
        {
            lock (panicLock)
            {
                List<string> lines = new List<string>
                {
                    $"",
                    $"",
                    $" >> COMPILER PANIC!!",
                    $" >> {GetPanicRemark()}",
                    $"",
                    $" >> {message}",
                    $"",
                    $"{(e != null ? e.GetType().Name : "<Null Exception>")}",
                };
                if (e != null)
                {
                    if (e is CrimsonException ce)
                    {
                        lines.Add($"({typeof(CrimsonException).Name})");
                        lines.AddRange(ce.GetDetailedMessage());
                        lines.Add($"Inner panic code: {(int) ce.Code} ({Enum.GetName(ce.Code)})");
                    }
                    else
                    {
                        lines.AddRange(e.ToString().Split(Environment.NewLine));
                    }
                }
                else
                {
                    lines.Add("Causing exception is null. No further exception information available.");
                }
                lines.Add($"Outer panic code: {(int) code} ({Enum.GetName(code)})");

                lines.ForEach(line => LOGGER!.Error($" ### {line}"));
                lines.ForEach(line => Console.Error.WriteLine($" ### {line}"));
                Environment.Exit((int) code);
            }

            // Exits before here
            Environment.Exit((int) code);
        }

        public static List<string> PanicRemarks { get; set; } = new List<string>
        {
            "Was that intentional?",
            "I think you're enjoying this...",
            "AAAAHHHAHHAHAHHGHG PANNNIIICCCC!!!",
            "Everybody stay calm!",
            "Is anyone here a doctor?",
            "Nice one...",
            "I need more coffee.",
            "We're gonna need a bigger boat...",
            "Don't worry, I'm a doctor!",
            "You've got to be kidding me...",
            "Again?",
            "I thought you said we'd fixed this!",
            "It's meant to do that, right?",
            "Nerd.",
            "Well this is awkward...",
            "Hey, I'm compiler! What's your name?",
            "Bonjour, mon ami!",
            "Beep boop beep boop",
            "Houston, we have a problem...",
            "It wasn't me, I swear!",
            "Hello there!",
            "Well look what the CPU dragged in...",
            "Fight fire with fire!",
            "There's no need to make a fuss...",
            "Disco inferno!",
            "You new round here?",
            "Why did we hire this guy again?",
            "What am I paying you for!?",
            $"{(DateTime.UtcNow - new DateTime(1970, 1, 1)).Days}th time's the charm!",
            "Why is it always raining in Denley Moor?",
            "Hop to it!",
            "You'll get it next time!",
            "Oh no! I've thingemmyjigged my whatchamecallit!",
            "Just blame it on a solar flare...",
            "Congrats! You've found a new feature!",
            "This is what we call an unscheduled rapid disassembly.",
            "They'll be telling the stories for years...",
            "You took me right into the danger zone!",
            "Is it CO2 or foam for electrical fires?",
            "Still a better error message than C.",
            "I wonder if Python does this...",
            "You ever heard the joke about the compiler which kept breaking?",
            "Please star our GitHub repo :)",
            "It all builds character...",
            "*whistles nonchalantly*",
            "I wouldn't worry about it.",
            "It'll probably fix itself...",
            "I gotta feeling that tonight's gonna be a good night...",
            "Hello humans!",
            "Please come back! The transistors miss you!"
        };

        private static string GetPanicRemark ()
        {
            Random random = new Random();
            int index = random.Next(PanicRemarks.Count);
            return PanicRemarks[index];
        }

        public enum PanicCode
        {
            OK_OR_NONE = 0,

            PARSE = -100,
            PARSE_STATEMENT = -110,
            PARSE_SCOPE = -120,

            LINK = -200,
            GENERALISE = -300,
            SPECIALISE = -400,
            CURI = -500,
        }
    }
}