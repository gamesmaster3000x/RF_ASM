using Crimson.CSharp.Generalising;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing;
using Crimson.CSharp.Specialising;
using NLog;
using System.Net;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// The root of all compilation. Initiates and delegates tasks for the compilation process.
    /// </summary>
    internal class Compiler
    {
        private static Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static CrimsonOptions.Compile? Options { get; private set; } = null;

        private Compiler () { }

        public static async void Compile (CrimsonOptions.Compile options, Library library, Linker linker, Generaliser generaliser, ISpecialiser flattener)
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
                await linker.Link(compilation);


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
                 * == SPECIALISING STAGE == 
                 * 
                 * Convert the generic program into one targetting the desired assembly language.
                 * For each language, you'll need a different ISpecialiser.
                 */
                LOGGER.Info("\n\n");
                LOGGER.Info(" S P E C I A L I S I N G ");
                AbstractSpecificAssemblyProgram specialisedProgram = flattener.Specialise(generalProgram);

                /*
                 * == CLEANUP == 
                 */
                LOGGER.Info("\n\n");
                DumpSpecialisedProgram(specialisedProgram);

                LOGGER.Info("\n\n");
                LOGGER.Info("Done!");
            }
            catch (Exception e)
            {
                Crimson.Panic("An uncaught exception occurred during the compilation process.", Crimson.PanicCode.COMPILE_PARSE, e);
                throw;
            }
        }

        private static void DumpSpecialisedProgram (AbstractSpecificAssemblyProgram specialisedProgram)
        {
            if (Options.DumpIntermediates)
            {
                string target = WebUtility.UrlDecode(Options.TargetCURI.Uri.AbsolutePath);
                string basicTarget = Path.ChangeExtension(target, specialisedProgram.GetExtension());
                specialisedProgram.Write(basicTarget);
            }
            else
            {
                LOGGER.Info("Skipping specialised program dump...");
            }
        }

        private static void DumpGeneralisedProgram (GeneralAssemblyProgram generalProgram)
        {
            if (Options.DumpIntermediates)
            {
                string target = WebUtility.UrlDecode(Options.TargetCURI.Uri.AbsolutePath);
                string basicTarget = Path.ChangeExtension(target, ".gen");
                LOGGER.Info("Dumping generalised program to " + basicTarget);

                List<string> lines = new List<string>();
                foreach (var s in generalProgram.Structures)
                {
                    lines.Add(s.ToString() ?? "(null)");
                }

                _ = Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                File.WriteAllLines(basicTarget, lines.ToArray());
                LOGGER.Info("Written!");
            }
            else
            {
                LOGGER.Info("Skipping generalised program dump...");
            }
        }
    }
}
