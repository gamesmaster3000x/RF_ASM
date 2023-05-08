using Compiler.Common.Exceptions;
using NLog;

namespace Compiler.Common
{
    public static partial class Panicker
    {
        private static readonly object panicLock = new object();

        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

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

        private static string GetPanicRemark ()
        {
            Random random = new Random();
            int index = random.Next(PanicRemarks.Count);
            return PanicRemarks[index];
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
    }
}