using CommandLine;
using NLog;
using NLog.Fluent;
using NLog.Layouts;

namespace Crimson.Core
{
    internal class CrimsonCompiler
    {
        public CrimsonCmdArguments Options { get; }

        private static NLog.Logger Logger;

        public CrimsonCompiler(CrimsonCmdArguments options)
        {
            Options = options;
        }

        static Task<int> Main(string[] args)
        {
            bool useAutowiredArgs = true;
            if (useAutowiredArgs)
            {
                args = new string[] { "-s" , "D:/ spaced thing", "-t", "out", "-n", "nope"};
            }

            // Start
            Console.WriteLine("");
            Console.WriteLine(" ~C~ "); 
            Console.WriteLine("Crimson Language Compiler, by GenElectrovise, for GamesMaster3000X");
            Console.WriteLine("https://github.com/gamesmaster3000x/RF_ASM");
            Console.WriteLine(" ~C~ ");
            Console.WriteLine("");

            Console.WriteLine("Parsing program arguments");
            return Parser.Default.ParseArguments<CrimsonCmdArguments>(args).MapResult((CrimsonCmdArguments options) =>
            {
                Console.WriteLine("CompilationSourcePath: " + options.CompilationSourcePath);
                Console.WriteLine("CompilationTargetPath: " + options.CompilationTargetPath);
                Console.WriteLine("NativeLibraryPath: " + options.NativeLibraryPath);
                CrimsonCompiler compiler = new CrimsonCompiler(options);
                return Task.FromResult(compiler.Start());
            },
            (error) => {
                Console.Error.WriteLine("An issue occurred while parsing the program arguments (invalid arguments)");
                return Task.FromResult(-1);
            });
        }

        private int Start()
        {
            ConfigureNLog();
            return 0;
        }

        private void ConfigureNLog()
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
            NLog.LogManager.Configuration = config;
            Console.WriteLine("NLog configured!");

            Logger = NLog.LogManager.GetCurrentClassLogger();
            Logger.Trace("Testing trace level logging...");
            Logger.Fatal("Testing fatal level logging...");
            Console.WriteLine("Did you see *both* of the *two* test messages? If not, you should report this to the developer!");
        }
    }
}