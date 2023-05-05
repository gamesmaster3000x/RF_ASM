using Compiler.Generalising;
using Compiler.Mapping;
using Compiler.Parsing;
using NLog;
using System.Net;

namespace Compiler.Common
{
    /// <summary>
    /// The root of all compilation. Initiates and delegates tasks for the compilation process.
    /// </summary>
    internal class Compiler
    {
        private static Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static CrimsonCoreOptions Options { get; private set; } = null;

        private Compiler() { }

        public static async void Compile(CrimsonCoreOptions options, Library library, Mapper mapper, Generaliser generaliser)
        {
            try
            {
                Options = options;

                /*
                 * == PARSING STAGE == 
                 * 
                 * The syntax of the source files must be parsed from text to an abstract syntax tree.
                 * In this case, the work is done by ANTLR, using the Crimson.g4 grammar file.
                 * This stage results in a collection of individual units which contain ComplexStatements exactly describing the input.
                 * 
                 */
                LOGGER.Info("\n\n");
                LOGGER.Info(" P A R S I N G ");
                Scope rootScope = library.LoadScope(Options.SourceCURI); // Get the root unit (ie. main.crm)
                Compilation compilation = new Compilation(library); // Generate dependency units (all resources are henceforth accessible)


                /*
                 * == LINKING STAGE == 
                 * 
                 * Now that all of the statements have been flattened into one list, we can iterate through and link the FunctionCalls.
                 * 
                 */
                LOGGER.Info("\n\n");
                LOGGER.Info(" L I N K I N G ");
                mapper.Map(compilation);
                // await mapper.Link(compilation);


                /*
                 * == GENERALISING STAGE == 
                 * 
                 * Converts the program into a list of general assembly statements covering the concepts of the program 
                 * without tying it to one assembly language.
                 */
                LOGGER.Info("\n\n");
                LOGGER.Info(" G E N E R A L I S I N G ");
                GeneralAssemblyProgram generalProgram = await generaliser.Generalise(compilation);


                LOGGER.Info("\n\n");
                DumpGeneralisedProgram(generalProgram);

                /*
                 * == CLEANUP == 
                 */

                LOGGER.Info("\n\n");
                LOGGER.Info("Done!");
            }
            catch (Exception e)
            {
                Program.Panic("An uncaught exception occurred during the compilation process.", Program.PanicCode.COMPILE_PARSE, e);
                throw;
            }
        }

        private static void DumpGeneralisedProgram(GeneralAssemblyProgram generalProgram)
        {
            if (Options.DumpIntermediates)
            {
                string target = WebUtility.UrlDecode(Options.TargetCURI.Uri.AbsolutePath);
                string basicTarget = Path.ChangeExtension(target, ".gen");
                LOGGER.Info("Dumping generalised program to " + basicTarget);

                List<string> lines = new List<string>();
                foreach (var s in generalProgram.Structures)
                    lines.Add(s.ToString() ?? "(null)");

                _ = Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                File.WriteAllLines(basicTarget, lines.ToArray());
                LOGGER.Info("Written!");
            }
            else
                LOGGER.Info("Skipping generalised program dump...");
        }
    }
}
