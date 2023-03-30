using Crimson.CSharp.Generalising;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing;
using Crimson.CSharp.Specialising;
using NLog;

namespace Crimson.CSharp.Core
{
    /// <summary>
    /// The root of all compilation. Initiates and delegates tasks for the compilation process.
    /// </summary>
    internal class CrimsonCompiler
    {
        private static Logger LOGGER = LogManager.GetCurrentClassLogger();
        public CrimsonOptions Options { get; }
        public FileOnlyLibrary Library { get; }
        public Linker Linker { get; }
        public Generaliser Generaliser { get; }
        public ISpecialiser Specialiser { get; }

        public CrimsonCompiler (CrimsonOptions options, FileOnlyLibrary unitGenerator, Linker linker, Generaliser generaliser, ISpecialiser flattener)
        {
            Options = options;
            Library = unitGenerator;
            Linker = linker;
            Generaliser = generaliser;
            Specialiser = flattener;
        }

        public int FullyCompileFromOptions ()
        {
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
            Task<Scope> rootScopeTask = Library.LoadScopeAsync(Options.TranslationSourcePath, true); // Get the root unit (ie. main.crm)
            rootScopeTask.Wait(); // Block until finished
            Scope rootScope = rootScopeTask.Result;
            Compilation compilation = new Compilation(rootScope, Options); // Generate dependency units (all resources are henceforth accessible)


            /*
             * == LINKING STAGE == 
             * 
             * Now that all of the statements have been flattened into one list, we can iterate through and link the FunctionCalls.
             * 
             */
            LOGGER.Info("\n\n");
            LOGGER.Info(" L I N K I N G ");
            Linker.Link(compilation);


            /*
             * == GENERALISING STAGE == 
             * 
             * Converts the program into a list of general assembly statements covering the concepts of the program 
             * without tying it to one assembly language.
             */
            LOGGER.Info("\n\n");
            LOGGER.Info(" G E N E R A L I S I N G ");
            GeneralAssemblyProgram generalProgram = Generaliser.Generalise(compilation);

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
            AbstractSpecificAssemblyProgram specialisedProgram = Specialiser.Specialise(generalProgram);

            /*
             * == CLEANUP == 
             */
            LOGGER.Info("\n\n");
            DumpSpecialisedProgram(specialisedProgram);

            LOGGER.Info("\n\n");
            LOGGER.Info("Done!");
            return 1;
        }

        private void DumpSpecialisedProgram (AbstractSpecificAssemblyProgram specialisedProgram)
        {
            if (Options.DumpIntermediates)
            {
                string basicTarget = Path.ChangeExtension(Options.TranslationTargetPath, specialisedProgram.GetExtension());
                specialisedProgram.Write(basicTarget);
            }
            else
            {
                LOGGER.Info("Skipping specialised program dump...");
            }
        }

        private void DumpGeneralisedProgram (GeneralAssemblyProgram generalProgram)
        {
            if (Options.DumpIntermediates)
            {
                string basicTarget = Path.ChangeExtension(Options.TranslationTargetPath, ".gen");
                LOGGER.Info("Dumping generalised program to " + basicTarget);

                List<string> lines = new List<string>();
                foreach (var s in generalProgram.Structures)
                {
                    lines.Add(s.ToString() ?? "(null)");
                }

                _ = Directory.CreateDirectory(Path.GetDirectoryName(Options.TranslationTargetPath)!);
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
