using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Compiler.Parsing;
using Compiler.Mapping;
using Compiler.Packing;
using NLog.Config;
using NLog;

ShowSplash();
ShowCredits();
ConfigureNLog();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IParser, DefaultParser>();
        services.AddSingleton<Mapper>();
        services.AddSingleton<Packer>();
        services.AddSingleton<IBerryCli, DefaultParser>();
    })
    .Build();

IParser parser = host.Services.GetRequiredService<IParser>();
parser.Parse();

host.Run();


static void ShowSplash ()
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

static void ShowCredits ()
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

static void ConfigureNLog ()
{
    Console.WriteLine("Configuring NLog...");
    LoggingConfiguration config = new LoggingConfiguration();
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

    Logger LOGGER = LogManager.GetCurrentClassLogger();
    Console.WriteLine("Testing TRACE and FATAL level logging...");
    LOGGER.Trace("Testing trace level logging...");
    LOGGER.Fatal("Testing fatal level logging...");
    Console.WriteLine("Did you see the TRACE and FATAL test messages?");
}

/*namespace Compiler
{

    public partial class Program
    {

        private static Logger? LOGGER;
        public static readonly string VERSION = "v0.0";

        public static IScopeProvider ScopeProvider;

        public static LogFactory LogFactory { get; private set; }
        public static string AssemblyLocation { get => Assembly.GetExecutingAssembly().Location; }
        private static LoggingConfiguration LogConfig { get; set; }

        static Program ()
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
                Mapper linker = new Mapper();
                Generaliser generaliser = new Generaliser();

                Common.Compiler.Compile(options, generator, linker, generaliser);
            }
            catch (Exception ex)
            {
                Panicker.Panic("Error executing verb 'compile'.", PanicCode.COMPILE, ex);
                throw;
            }
        }


        
    }
}*/