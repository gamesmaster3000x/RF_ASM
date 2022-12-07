using CommandLine;
using NLog;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Statements;
using System.IO.Abstractions;

namespace Crimson.CSharp.Core
{
    internal class CrimsonCompiler
    {
        public CrimsonCmdArguments Options { get; }

        private static Logger LOGGER;

        public CrimsonCompiler(CrimsonCmdArguments options)
        {
            Options = options;
        }

        static Task<int> Main(string[] args)
        {
            bool useAutowiredArgs = true;
            if (useAutowiredArgs)
            {
                string testProgramsPath = "../../../Resources/Documentation/Examples/"; // Escape bin, Debug, and net6.0
                args = new string[] { "-s", testProgramsPath + "Main_Utils/main.crm", "-t", "out", "-n", "nope" };
            }

            // Start
            Console.WriteLine("");
            Console.WriteLine(" ~C~ ");
            Console.WriteLine("Crimson Language Compiler, by GenElectrovise, for GamesMaster3000X");
            Console.WriteLine("https://github.com/gamesmaster3000x/RF_ASM");
            Console.WriteLine(" ~C~ ");
            Console.WriteLine("");

            Console.WriteLine("Parsing program arguments");
            return CommandLine.Parser.Default.ParseArguments<CrimsonCmdArguments>(args).MapResult((options) =>
            {
                Console.WriteLine("CompilationSourcePath: " + options.CompilationSourcePath);
                Console.WriteLine("CompilationTargetPath: " + options.CompilationTargetPath);
                Console.WriteLine("NativeLibraryPath: " + options.NativeLibraryPath);
                CrimsonCompiler compiler = new CrimsonCompiler(options);
                return Task.FromResult(compiler.Start(options));
            },
            (error) =>
            {
                Console.Error.WriteLine("An issue occurred while parsing the program arguments (invalid arguments)");
                return Task.FromResult(-1);
            });
        }

        private int Start(CrimsonCmdArguments options)
        {
            // Prepare
            ConfigureNLog();

            string programText = string.Join(Environment.NewLine, File.ReadLines(Options.CompilationSourcePath));
            /* Compilation fullCompilationObject = */ ParseProgram(programText);

            if (options.CleanFiles)
            {
                LOGGER.Error("Cleaning of files is not implemented");
            }
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
            LogManager.Configuration = config;
            Console.WriteLine("NLog configured!");

            LOGGER = LogManager.GetCurrentClassLogger();
            Console.WriteLine("Testing TRACE and FATAL level logging...");
            LOGGER.Trace("Testing trace level logging...");
            LOGGER.Fatal("Testing fatal level logging...");
            Console.WriteLine("Did you see the TRACE and FATAL test messages?");
        }

        private void /* Return full compilation object later */ ParseProgram(string textIn)
        {
            // Get Antlr context
            AntlrInputStream a4is = new AntlrInputStream(textIn);
            CrimsonLexer lexer = new CrimsonLexer(a4is);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            CrimsonParser parser = new CrimsonParser(cts);

            CrimsonParser.CompilationUnitContext cuCtx = parser.compilationUnit();
            CrimsonCompiliationUnitVisitor visitor = new CrimsonCompiliationUnitVisitor();
            CompilationUnit compilation = visitor.VisitCompilationUnit(cuCtx);

            return;
        }
    }
}