
#define CORE_DEBUG

using NLog;
using CrimsonCore.Specialising.RFASM;
using System.Reflection;
using CrimsonCore.Core;
using CrimsonCore.Exceptions;
using CrimsonCore.Specialising;
using CrimsonCore.Generalising;
using CrimsonCore.Linking;
using NLog.Config;

namespace CrimsonCore
{

    public class CrimsonCore
    {

        private static Logger? LOGGER;
        public static readonly string VERSION = "v0.0";

        public static IScopeProvider ScopeProvider;

        public static LogFactory LogFactory { get; private set; }
        public static string AssemblyLocation { get => Assembly.GetExecutingAssembly().Location; }
        private static LoggingConfiguration LogConfig { get; set; }

        static CrimsonCore ()
        {
            LogFactory = new LogFactory();
            string _assemblyFolder = Path.GetDirectoryName(AssemblyLocation);
            LogConfig = new XmlLoggingConfiguration(_assemblyFolder + "\\ProjectX.exe.nlog", LogFactory);

            LogFactory.Configuration = LogConfig;
        }

        public static void Compile (CrimsonCoreOptions options)
        {
            LOGGER!.Info($"Compiling with: {options}");
            try
            {
                Console.WriteLine("  Compile: SourceUri: " + options.SourceCURI);
                Console.WriteLine("  Compile: TargetUri: " + options.TargetCURI);
                Console.WriteLine("  Compile: NativeUri: " + options.NativeCURI);
                Console.WriteLine("  Compile: DumpIntermediates: " + options.DumpIntermediates);
                Console.WriteLine("  Compile: DataWidth: " + options.DataWidth);

                ShowSplash();
                ShowCredits();
                ConfigureNLog();

                Library generator = new Library();
                Linker linker = new Linker();
                Generaliser generaliser = new Generaliser();
                ISpecialiser specialiser = new RFASMSpecialiser(); //TODO Don't default specialiser to RFASM

                Compiler.Compile(options, generator, linker, generaliser, specialiser);
            }
            catch (Exception ex)
            {
                Panic("Error executing verb 'compile'.", PanicCode.COMPILE, ex);
                throw;
            }
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
                Layout = "${level:uppercase=true:padding=-5} | ${time} | ${threadname:whenEmpty=${threadid}:padding=-6} | ${logger} > ${message:withexception=true}"
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

        private static void ConfigureMultithreading ()
        {
        }

        //

        public static string GetRoamingPath (string path)
        {
            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string relative = $"Crimson/{path}";
            string combined = Path.Combine(roaming, relative);
            string full = Path.GetFullPath(combined);
            return full;
        }

        public static FileInfo GetRoamingFile (string path)
        {
            string betterPath = GetRoamingPath(path);
            return new FileInfo(betterPath);
        }

        public static DirectoryInfo GetRoamingDirectory (string path)
        {
            string betterPath = GetRoamingPath(path);
            return new DirectoryInfo(betterPath);
        }


        // ================= PANIC =================


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
                    $"{(e != null ? e.GetType().Name : "<Exception is Null>")} {(e is CrimsonCoreException ? $"({typeof(CrimsonCoreException).Name})" : "")}",
                };
                if (e != null)
                    if (e is CrimsonCoreException ce)
                    {
                        lines.AddRange(ce.GetDetailedMessage());
                        lines.Add($"Inner panic code: {(int) ce.Code} ({Enum.GetName(ce.Code)})");
                    }
                    else
                        lines.AddRange(e.ToString().Split(Environment.NewLine));
                else
                    lines.Add("Causing exception is null. No further exception information available.");
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
            ARGUMENTS = -10,
            SETTINGS = -20,

            // CURI
            CURI = -100,
            CURI_STREAM = -110,

            // Compile
            COMPILE = -1000,
            COMPILE_PARSE = -1100,
            COMPILE_PARSE_STATEMENT = -1110,
            COMPILE_PARSE_SCOPE = -1120,
            COMPILE_PARSE_SCOPE_ASYNC = -1121,
            COMPILE_PARSE_SCOPE_DEPS = -1122,
            COMPILE_LINK = -1200,
            COMPILE_GENERALISE = -1300,
            COMPILE_SPECIALISE = -1400,

            // Cache
            CACHE = -2000,
            CACHE_JSON = -2010,
            CACHE_FETCH = -2020,
            CACHE_ADD = -2021,
            CACHE_INSTALL = -2100,
            CACHE_CLEAR = -2200,
        }
    }
}