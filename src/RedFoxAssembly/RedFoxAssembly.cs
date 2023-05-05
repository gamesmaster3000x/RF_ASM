using CommandLine;
using NLog;
using Antlr4.Runtime;
using RedFoxAssembly.AntlrBuild;
using RedFoxAssembly.Statements;
using RedFoxAssembly.Core;
using RedFoxAssembly.Antlr;

namespace RedFoxAssembly
{
    internal class RedFoxAssembly
    {

        private static Logger? LOGGER;
        internal static readonly string VERSION = "v0.0";

        public static readonly bool USE_AUTOWIRED_ARGUMENTS = true;
        public static readonly string TEST_PROGRAMS_PATH = "../../../Documentation/TestPrograms/"; // Escape bin, Debug, and net6.0

        static Task<int> Main (string[] args)
        {
            if (USE_AUTOWIRED_ARGUMENTS)
            {
                Console.WriteLine("Using autowired (debug) program arguments");
                args = new string[] {
                    "-s", TEST_PROGRAMS_PATH + "TestAll.txt",
                    "-w", "2",
                    "-m", TEST_PROGRAMS_PATH + "TestAllMetadata.json"
                };
            }

            // Start
            ShowSplash();
            ShowCredits();

            Console.WriteLine("Parsing RFASM options");
            return CommandLine.Parser.Default.ParseArguments<RFASMOptions>(args).MapResult((options) =>
            {
                Console.WriteLine("  Option: InputPath: " + options.SourcePath);
                Console.WriteLine("  Option: DataWidth: " + options.DataWidth);
                Console.WriteLine("  Option: MetadataPath: " + options.MetadataPath);

                ConfigureNLog();
                LOGGER = LogManager.GetCurrentClassLogger();

                return Task.FromResult(StartFromOptions(options));
            },
            (error) =>
            {
                Console.Error.WriteLine("An issue occurred while parsing the program arguments (invalid arguments)");
                return Task.FromResult(-1);
            });
        }

        private static int StartFromOptions (RFASMOptions options)
        {
            LOGGER!.Info("Starting RFASM compilation from options " + options);

            LOGGER!.Info("Reading input file...");
            string[] rawLinesArr = File.ReadAllLines(options.SourcePath);
            LOGGER!.Info($"Found {rawLinesArr.Length} lines");

            //ITokenGenerator generator = new RFASMTokenGenerator(meta);
            //TokenParser.TokenParser parser = new TokenParser.TokenParser(RFASMTokenGenerator.GOOD_TOKEN, RFASMTokenGenerator.IGNORE_TOKEN, RFASMTokenGenerator.BAD_TOKEN, meta, generator);
            //List<IToken> tokens = parser.Parse(rawLines);
            //string tokenHash = ComputeTokenListHash(tokens);
            //Console.WriteLine("Hash of token raw values: " + tokenHash);

            LOGGER!.Info("Setting ANTLR loose...");
            // Get Antlr context
            RFASMProgram program;
            {
                AntlrInputStream a4is = new AntlrInputStream(string.Join(Environment.NewLine, rawLinesArr));
                RedFoxAssemblyLexer lexer = new RedFoxAssemblyLexer(a4is);
                // lexer.AddErrorListener(new LexerErrorListener(meta.InputPath));

                CommonTokenStream cts = new CommonTokenStream(lexer);
                RedFoxAssemblyParser parser = new RedFoxAssemblyParser(cts);
                // parser.ErrorHandler = new BailErrorStrategy();
                parser.AddErrorListener(new ParserErrorListener(options.SourcePath));

                RedFoxAssemblyParser.ProgramContext cuCtx = parser.program();
                RFASMProgramVisitor visitor = new RFASMProgramVisitor();
                program = visitor.VisitProgram(cuCtx);
            }

            if (program == null)
                throw new NullReferenceException("Why is the program null :(");
            LOGGER!.Info("We have a program!");

            LOGGER!.Info("Creating compiler using input options (they haven't changed)");
            RFASMCompiler compiler = new RFASMCompiler(options);

            LOGGER!.Info("Say goodbye to the RFASM entry-point! (Delegating compilation to RFASMCompiler)");
            return compiler.Compile(program);
        }

        private static void ShowSplash ()
        {
            Console.WriteLine("");
            Console.WriteLine("RFASM Compiler, by GenElectrovise, for GamesMaster3000X");
            Console.WriteLine("                                                                                                      ");
            Console.WriteLine("  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  ");
            Console.WriteLine(" | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. | ");
            Console.WriteLine(" | |  _______     | || |  _________   | || |      __      | || |    _______   | || | ____    ____ | | ");
            Console.WriteLine(" | | |_   __ \\    | || | |_   ___  |  | || |     /  \\     | || |   /  ___  |  | || ||_   \\  /   _|| | ");
            Console.WriteLine(" | |   | |__) |   | || |   | |_  \\_|  | || |    / /\\ \\    | || |  |  (__ \\_|  | || |  |   \\/   |  | | ");
            Console.WriteLine(" | |   |  __ /    | || |   |  _|      | || |   / ____ \\   | || |   '.___`-.   | || |  | |\\  /| |  | | ");
            Console.WriteLine(" | |  _| |  \\ \\_  | || |  _| |_       | || | _/ /    \\ \\_ | || |  |`\\____) |  | || | _| |_\\/_| |_ | | ");
            Console.WriteLine(" | | |____| |___| | || | |_____|      | || ||____|  |____|| || |  |_______.'  | || ||_____||_____|| | ");
            Console.WriteLine(" | |              | || |              | || |              | || |              | || |              | | ");
            Console.WriteLine(" | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' | ");
            Console.WriteLine("  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  ");
            Console.WriteLine("                                                                                                      ");
            Console.WriteLine("https://github.com/gamesmaster3000x/RF_ASM");
            Console.WriteLine("");
        }

        private static void ShowCredits ()
        {
            Console.WriteLine("");
            Console.WriteLine("  -> C R E D I T S <-  ");
            Console.WriteLine("Created by GenElectrovise https://github.com/GenElectrovise on behalf of GamesMaster3000X https://github.com/gamesmaster3000x");
            Console.WriteLine("ASCII art generated with https://patorjk.com/software/taag/");
            Console.WriteLine("ASCII art font 'Blocks' by myflix");
            Console.WriteLine("Using NuGet packages: ANTLR4, CommandLineParser, System.IO.Abstractions and NLog");
            Console.WriteLine("Written using Visual Studio in C#/.NET 6.0 (LTS)");
            Console.WriteLine("  -> - - - - - - - <-  ");
            Console.WriteLine("");
        }

        private static void ConfigureNLog ()
        {
            Console.WriteLine("Configuring NLog...");
            NLog.Config.LoggingConfiguration config = new NLog.Config.LoggingConfiguration();
            var fileTarget = new NLog.Targets.FileTarget("RFASMFileLogTarget")
            {
                FileName = "RFASM_${shortdate}.log",
                Layout = "${level} | ${time} | ${logger} > ${message:withexception=true}",
                DeleteOldFileOnStartup = true
            };
            var consoleTarget = new NLog.Targets.ConsoleTarget("RFASMConsoleLogTarget")
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