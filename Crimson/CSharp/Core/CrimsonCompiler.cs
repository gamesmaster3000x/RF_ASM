using CommandLine;
using NLog;
using Antlr4.Runtime;
using Crimson.AntlrBuild;
using Crimson.CSharp.Statements;

namespace Crimson.CSharp.Core
{
    internal class CrimsonCompiler
    {
        public CrimsonCmdArguments Options { get; }
        public Cleaner Cleaner { get; }

        private static Logger Logger;

        public CrimsonCompiler(CrimsonCmdArguments options)
        {
            Options = options;
            Cleaner = new Cleaner(Path.GetDirectoryName(options.CompilationSourcePath) + "/" + Path.GetFileNameWithoutExtension(options.CompilationSourcePath) + "_compiler_cleaner_temp/");
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
            CompilationUnit program = ParseProgram(programText);

            // Pre-compilation
            // LazySourceFile compilation = new LazySourceFile(options.CompilationSourcePath, options);

            if (options.CleanFiles)
            {
                Cleaner.CleanFiles();
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

            Logger = LogManager.GetCurrentClassLogger();
            Console.WriteLine("Testing TRACE and FATAL level logging...");
            Logger.Trace("Testing trace level logging...");
            Logger.Fatal("Testing fatal level logging...");
            Console.WriteLine("Did you see the TRACE and FATAL test messages?");
        }

        private CompilationUnit ParseProgram(string textIn)
        {
            // Get Antlr context
            AntlrInputStream a4is = new AntlrInputStream(textIn);
            CrimsonLexer lexer = new CrimsonLexer(a4is);
            CommonTokenStream cts = new CommonTokenStream(lexer);
            CrimsonParser parser = new CrimsonParser(cts);

            CrimsonParser.CompilationUnitContext cuCtx = parser.compilationUnit();
            CrimsonCompiliationUnitVisitor visitor = new CrimsonCompiliationUnitVisitor();
            CompilationUnit compilation = visitor.VisitCompilationUnit(cuCtx);
            //CrimsonListener listener = new CrimsonListener();
            //ParseTreeWalker.Default.Walk(listener, cuCtx);

            // Create packages

            return compilation;
        }
    }
}