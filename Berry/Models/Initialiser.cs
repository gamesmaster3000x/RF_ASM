using Berry.DB;
using NLog;
using System.Diagnostics;

namespace Berry.Models
{
    public class Initialiser
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static void Initialise (BerryDbContext context)
        {
            // Users
            if (!context.Users.Any())
            {
                var students = new UserEntry[]
                {
                new UserEntry{ Name="Admin" },
                };
                context.Users.AddRange(students);
                context.SaveChanges();
                LOGGER.Info("Initialised Users");
            }


            // Berries
            if (!context.Berries.Any())
            {
                var berries = new BerryEntry[]
                {
                };
                context.Berries.AddRange(berries);
                context.SaveChanges();
                LOGGER.Info("Initialised Berries");
            }
        }
    }
}
