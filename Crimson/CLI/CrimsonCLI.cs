﻿#define CLI_DEBUG

using CommandLine;
using NLog.Config;
using NLog;
using System.Reflection;
using Compiler.Common;

namespace CLI
{
    public class CrimsonCLI
    {

        public static int Main (string[] args)
        {
            try
            {
                // Setup arguments
#if CLI_DEBUG
                args = GetTestArguments() ?? args;
#endif
                Console.WriteLine(string.Join(' ', args));

                LOGGER!.Info("Parsing input arguments!");
                var verbs = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
                var result = Parser.Default.ParseArguments<
                        CrimsonCLIOptions.Compile,
                        CrimsonCLIOptions.Clear,
                        CrimsonCLIOptions.Install,
                        CrimsonCLIOptions.Refresh
                        >(args)
                    .WithParsed<CrimsonCLIOptions.Compile>(options => Compile(options))
                    .WithParsed<CrimsonCLIOptions.Install>(options => Install(options))
                    .WithParsed<CrimsonCLIOptions.Clear>(options => Clear(options))
                    .WithParsed<CrimsonCLIOptions.Refresh>(options => Refresh(options));
            }
            catch (Exception ex)
            {
                Panicker.Panic("An uncaught error occurred during program execution.", PanicCode.ARGUMENTS, ex);
                throw;
            }
            return 0;
        }

        private static Logger LOGGER;
        public static LogFactory LogFactory { get; private set; }
        public static string AssemblyLocation { get => Assembly.GetExecutingAssembly().Location; }
        private static LoggingConfiguration LogConfig { get; set; }

        static CrimsonCLI ()
        {
            LogFactory = new LogFactory();
            string _assemblyFolder = Path.GetDirectoryName(AssemblyLocation);
            LogConfig = new XmlLoggingConfiguration(_assemblyFolder + "\\ProjectX.exe.nlog", LogFactory);

            LogFactory.Configuration = LogConfig;
            LOGGER = LogFactory.GetCurrentClassLogger();
        }

        static CachedBerryClient CACHED_CLIENT = null;


        // ================= STARTUP =================
#if CLI_DEBUG
        private static string[] GetTestArguments ()
        {
            Console.WriteLine("OVERRIDING INPUT ARGUMENTS FOR TESTING");

            bool COMPILE = true;
            if (COMPILE)
                return new string[] {
                    $"compile",
                    $"-s relative:///Resources/Test Compilations/main.crm",
                    $"-t relative:///Resources/Test Compilations/result/main",
                    $"-n relative:///Resources/Native Library/",
                    $"-w 4"
                };

            bool INSTALL = false;
            if (INSTALL)
                return new string[] {
                    $"install",
                    $"-s http://raw.githubusercontent.com/GenElectrovise/RF_ASM/master/Crimson/Resources/Native%20Library/heap.crm",
                    //$"-o"
                };

            bool CLEAR = false;
            if (CLEAR)
                return new string[] {
                    $"clear",
                    $"-e",
                    //$"-i"
                    //$"-u"
                };

            bool REFRESH = false;
            if (REFRESH)
                return new string[] {
                    $"refresh",
                    $"-s http://raw.githubusercontent.com/GenElectrovise/RF_ASM/master/Crimson/Resources/Native%20Library/heap.crm",
                    //$"-a"
                };

            return null!;
        }
#endif

        private static void Compile (CrimsonCLIOptions.Compile options)
        {
            throw new NotImplementedException();
        }


        private static void Install (CrimsonCLIOptions.Install options)
        {
            LOGGER!.Info($"Installing with: {options}");
            try
            {
                CACHED_CLIENT.Install(options.SourceCURI, options.Overwrite);
            }
            catch (Exception ex)
            {
                Panicker.Panic("Error executing verb 'install'.", PanicCode.CACHE_INSTALL, ex);
                throw;
            }
        }

        private static void Clear (CrimsonCLIOptions.Clear options)
        {
            LOGGER!.Info($"Clearing with: {options}");
            try
            {
                CACHED_CLIENT.Clear(options.ClearMode);
            }
            catch (Exception ex)
            {
                Panicker.Panic("Error executing verb 'clear'.", PanicCode.CACHE_CLEAR, ex);
                throw;
            }
        }

        private static void Refresh (CrimsonCLIOptions.Refresh options)
        {
            LOGGER!.Info($"Refreshing with: {options}");
            try
            {
                CACHED_CLIENT.Refresh(options.SourceCURI, options.All);
            }
            catch (Exception ex)
            {
                Panicker.Panic("Error executing verb 'clear'.", PanicCode.CACHE_CLEAR, ex);
                throw;
            }
        }
    }
}